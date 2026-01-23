using UnityEngine;
using TMPro;

public class BallsLeftUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ballsLeftText;

    private void OnEnable()
    {
        CountBalls.onBallsLeftChanged += UpdateText;
    }

    private void OnDisable()
    {
        CountBalls.onBallsLeftChanged -= UpdateText;
    }

    private void Start()
    {
        
        if (CountBalls.Instance != null)
        {
            UpdateText(CountBalls.Instance.BallsLeft);
        }
    }

    private void UpdateText(int ballsLeft)
    {
        ballsLeftText.text = $"Balls Left: {ballsLeft}";
    }
}
