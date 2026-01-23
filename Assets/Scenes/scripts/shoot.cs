using UnityEngine;
using System;   

public class Shoot : MonoBehaviour
{
    
    public static event Action onShootBall;

    
    [SerializeField] private float lineSpeed = 10f;
    [SerializeField] private float maxLineLength = 5f;

    
    [SerializeField] private GameObject prefab;
    [SerializeField] private float forceBuild = 20f;
    [SerializeField] private float maximumHoldTime = 5f;

    
    private LineRenderer _line;

   
    private bool _lineActive = false;
    private bool _shotEnabled = true;

    private float _pressTimer = 0f;
    private float _launchForce = 0f;

    
    private void Start()
    {
        // LineRenderer setup
        _line = GetComponent<LineRenderer>();
        _line.SetPosition(1, Vector3.zero);

        // Listen for ball depletion
        CountBalls.onBallsDepleted += DisableShot;
    }

    private void OnDisable()
    {
        CountBalls.onBallsDepleted -= DisableShot;
    }

    private void Update()
    {
        // Only allow shooting if enabled
        if (_shotEnabled)
        {
            HandleShot();
        }
    }

    // ===== SHOOT LOGIC =====
    private void HandleShot()
    {
        // Mouse button pressed
        if (Input.GetMouseButtonDown(0))
        {
            _pressTimer = 0f;
            _lineActive = true;
        }

        // Mouse button released
        if (Input.GetMouseButtonUp(0))
        {
            _launchForce = _pressTimer * forceBuild;

            GameObject ball = Instantiate(prefab, transform.parent);
            ball.transform.rotation = transform.rotation;
            ball.transform.position = transform.position;

            ball.GetComponent<Rigidbody2D>()
                .AddForce(ball.transform.right * _launchForce, ForceMode2D.Impulse);

            // Reset line
            _lineActive = false;
            _line.SetPosition(1, Vector3.zero);

            // Invoke shoot event
            onShootBall?.Invoke();
        }

        // Build up press timer
        if (_pressTimer < maximumHoldTime)
        {
            _pressTimer += Time.deltaTime;
        }

        // Update line length (clamped)
        if (_lineActive)
        {
            float lineLength = _pressTimer * lineSpeed;
            lineLength = Mathf.Clamp(lineLength, 0f, maxLineLength);
            _line.SetPosition(1, Vector3.right * lineLength);
        }
    }

    
    private void DisableShot()
    {
        _shotEnabled = false;

        // Safety: hide line if balls run out while charging
        _lineActive = false;
        _line.SetPosition(1, Vector3.zero);
    }
}
