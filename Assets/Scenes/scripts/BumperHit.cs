using System;
using UnityEngine;

public class BumperHit : MonoBehaviour
{
    [SerializeField] private int scoreValue = 100;

    
    public static event Action<Transform, int> onBumperHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // stuur de transform van de bumper mee
            onBumperHit?.Invoke(transform, scoreValue);
        }
    }
}
