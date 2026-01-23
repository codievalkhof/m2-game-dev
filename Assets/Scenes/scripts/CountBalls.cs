using System;
using UnityEngine;

public class CountBalls : MonoBehaviour
{
    public static CountBalls Instance;

    public static event Action<int> onBallsLeftChanged;
    public static event Action onBallLost;
    public static event Action onBallsDepleted;

    [SerializeField] private int ballsLeft = 5;

    private void Awake()
    {
        
        Instance = this;
    }

    private void Start()
    {
        Shoot.onShootBall += CountOnShot;

        
        onBallsLeftChanged?.Invoke(ballsLeft);
    }

    private void OnDisable()
    {
        Shoot.onShootBall -= CountOnShot;
    }

    private void CountOnShot()
    {
        if (ballsLeft > 0)
        {
            ballsLeft--;
            onBallsLeftChanged?.Invoke(ballsLeft);
        }
        else
        {
            onBallsDepleted?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            onBallLost?.Invoke();
            Destroy(collision.gameObject);
        }
    }

    public int BallsLeft => ballsLeft;
}
