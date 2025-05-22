using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    public PowerUpType powerUpType;
    public Sprite InactiveSprite;
    public Sprite ActiveSprite;
    public Image[] Icons;
    public int MaxCount = 1;

    public SnakeController snakeController;

    private void Update()
    {
        int count = GetPowerUpCount();

        for (int i = 0; i < Icons.Length; i++)
        {
            Icons[i].sprite = i < count ? ActiveSprite : InactiveSprite;
            Icons[i].enabled = i < MaxCount;
        }
    }

    private int GetPowerUpCount()
    {
        switch (powerUpType)
        {
            case PowerUpType.Shield:
                return snakeController.HasShield ? 1 : 0;
            case PowerUpType.ScoreBoost:
                return snakeController.IsScoreBoostActive ? 1 : 0;
            case PowerUpType.SpeedUp:
                return snakeController.IsSpeedUpActive ? 1 : 0;
            default:
                return 0;
        }
    }
}