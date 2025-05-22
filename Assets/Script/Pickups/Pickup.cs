using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PowerUpType Type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController Snake = collision.GetComponent<SnakeController>();
        if (Snake != null)
        {
            switch (Type)
            {
                case PowerUpType.Shield:
                    Snake.ActivateShield();
                    break;
                case PowerUpType.ScoreBoost:
                    Snake.ActivateScoreBoost();
                    break;
                case PowerUpType.SpeedUp:
                    Snake.ActivateSpeedUp();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
