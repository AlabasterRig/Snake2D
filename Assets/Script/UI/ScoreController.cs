using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI ScoreTextPro;
    void Update()
    {
        ScoreTextPro.text = "Score: " + SnakeController.Instance.Score.ToString();
    }
}
