using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class SnakeController : MonoBehaviour
{
    private static SnakeController instance;
    public static SnakeController Instance { get { return instance; } }

    public Vector2 SnakeDirection = Vector2.right;
    private SpriteRenderer sr;

    public List<Transform> SnakeBodySegments;
    public Transform SnakeBodyPrefab;
    public Transform BorderTransformRight;
    public Transform BorderTransformLeft;
    public Transform BorderTransformTop;
    public Transform BorderTransformBottom;

    public float MoveInterval = 0.2f;
    private float MoveTimer;
    public bool HasShield = false;
    public bool IsScoreBoostActive = false;
    public bool IsSpeedUpActive = false;

    public float PowerUpDuration = 6f;
    private float PowerUpTimerShield;
    private float PowerUpTimerScore;
    private float PowerUpTimerSpeed;

    public int Score = 0;


    private void Awake()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SnakeBodySegments = new List<Transform>();
        SnakeBodySegments.Add(this.transform);
    }

    private void Update()
    {
        HandleInput();
        UpdatePowerUpTimers();
        FlipSnake();
    }

    private void FixedUpdate()
    {
        MoveTimer += Time.fixedDeltaTime;
        if (MoveTimer >= MoveInterval)
        {
            MoveSnake();
            MoveTimer = 0f;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && SnakeDirection != Vector2.down)
        {
            SnakeDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && SnakeDirection != Vector2.up)
        {
            SnakeDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && SnakeDirection != Vector2.right)
        {
            SnakeDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && SnakeDirection != Vector2.left)
        {
            SnakeDirection = Vector2.right;
        }
    }

    private void MoveSnake()
    {
        for (int i = SnakeBodySegments.Count - 1; i > 0; i--)
        {
            SnakeBodySegments[i].position = SnakeBodySegments[i - 1].position;
        }
        Vector3 NewPosition = transform.position + new Vector3(SnakeDirection.x, SnakeDirection.y, 0);
        NewPosition = WrapPosition(NewPosition);
        transform.position = NewPosition;
    }

    public void FlipSnake()
    {
        if (SnakeDirection == Vector2.left)
        {
            sr.flipX = true;
        }
        else if (SnakeDirection == Vector2.right)
        {
            sr.flipX = false;
        }
    }

    public void GrowSnake(int Amount = 1)
    {
        for (int i = 0; i < Amount; i++)
        {
            Transform NewSegment = Instantiate(SnakeBodyPrefab);
            NewSegment.position = SnakeBodySegments[SnakeBodySegments.Count - 1].position;
            SnakeBodySegments.Add(NewSegment);
        }

        Score += IsScoreBoostActive ? 2 : 1;
        Debug.Log("Score: " + Score);
    }

    private Vector3 WrapPosition(Vector3 Position)
    {

        if (Position.x > BorderTransformRight.position.x)
        {
            Position.x = BorderTransformLeft.position.x;
        }
        else if (Position.x < BorderTransformLeft.position.x)
        {
            Position.x = BorderTransformRight.position.x;
        }

        if (Position.y > BorderTransformTop.position.y)
        {
            Position.y = BorderTransformBottom.position.y;
        }
        else if (Position.y < BorderTransformBottom.position.y)
        {
            Position.y = BorderTransformTop.position.y;
        }

        return Position;
    }

    public void ShrinkSnake(int amount = 1)
    {
        if (HasShield)
        {
            HasShield = false;
            PowerUpTimerShield = 0f;
            return;
        }
        for (int i = 0; i < amount && SnakeBodySegments.Count > 1; i++)
        {
            Transform lastSegment = SnakeBodySegments[SnakeBodySegments.Count - 1];
            SnakeBodySegments.RemoveAt(SnakeBodySegments.Count - 1);
            Destroy(lastSegment.gameObject);
        }
    }

    private void UpdatePowerUpTimers()
    {
        if (HasShield)
        {
            PowerUpTimerShield -= Time.deltaTime;
            if (PowerUpTimerShield <= 0f)
            {
                HasShield = false;
                Debug.Log("Shield expired.");
            }
        }

        if (IsScoreBoostActive)
        {
            PowerUpTimerScore -= Time.deltaTime;
            if (PowerUpTimerScore <= 0f)
            {
                IsScoreBoostActive = false;
            }
        }

        if (IsSpeedUpActive)
        {
            PowerUpTimerSpeed -= Time.deltaTime;
            if (PowerUpTimerSpeed <= 0f)
            {
                IsSpeedUpActive = false;
                MoveInterval = 0.2f;
            }
        }
    }

    public void ActivateShield()
    {
        HasShield = true;
        PowerUpTimerShield = PowerUpDuration;
    }

    public void ActivateScoreBoost()
    {
        IsScoreBoostActive = true;
        PowerUpTimerScore = PowerUpDuration;
    }

    public void ActivateSpeedUp()
    {
        IsSpeedUpActive = true;
        PowerUpTimerSpeed = PowerUpDuration;
        MoveInterval = 0.1f;
    }

    public void RestartGame()
    {
        for (int i = 1; i < SnakeBodySegments.Count; i++)
        {
            Destroy(SnakeBodySegments[i].gameObject);
        }
        SnakeBodySegments.Clear();
        SnakeBodySegments.Add(this.transform);
        transform.position = Vector2.zero;
        SnakeDirection = Vector2.right;
        Score = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Segment"))
        {
            if (!HasShield)
            {
                RestartGame();
            }
        }
    }
}
