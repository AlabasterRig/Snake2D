using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodType Type;
    public int LengthDelta = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeController Snake = collision.GetComponent<SnakeController>();
        if (Snake != null)
        {
            if (Type == FoodType.MassGainer)
            {
                Snake.GrowSnake(LengthDelta);
            }
            else if (Type == FoodType.MassBurner && Snake.SnakeBodySegments.Count > 1)
            {
                Snake.ShrinkSnake(LengthDelta);
            }
            Destroy(gameObject);
        }
    }
}
