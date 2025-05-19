using UnityEngine;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    public Vector2 SnakeDirection = Vector2.right;
    private SpriteRenderer sr;

    public List<Transform> SnakeBodySegments;
    public Transform SnakeBodyPrefab;
    public Transform BorderTransformRight;
    public Transform BorderTransformLeft;
    public Transform BorderTransformTop;
    public Transform BorderTransformBottom;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SnakeBodySegments = new List<Transform>();
        SnakeBodySegments.Add(this.transform);
    }

    private void Update()
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

        FlipSnake();
    }

    private void FixedUpdate()
    {
        MoveSnake();
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

    public void GrowSnake()
    {
        Transform NewSegment = Instantiate(this.SnakeBodyPrefab);
        NewSegment.position = SnakeBodySegments[SnakeBodySegments.Count - 1].position;
        SnakeBodySegments.Add(NewSegment);
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

    public void RestartGame()
    {
        for (int i = 1; i < SnakeBodySegments.Count; i++)
        {
            Destroy(SnakeBodySegments[i].gameObject);
        }
        SnakeBodySegments.Clear();
        SnakeBodySegments.Add(this.transform);
        this.transform.position = Vector2.zero;
        this.SnakeDirection = Vector2.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PointController>() != null)
        {
            GrowSnake();
        }
        else if(collision.tag == "Segment")
        {
            RestartGame();
        }
    }
}
