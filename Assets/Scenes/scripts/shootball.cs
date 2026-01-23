using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    // Hoe hard de bal wordt weggeduwd
    public float ShootForce = 500f;

    // In welke richting de bal duwt krijgt (0,1,0 = omhoog)
    public Vector3 Direction = new Vector3(0f, 1f, 0f);

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // aalt automatisch de Rigidbody2D op van dit object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Geeft een kracht in de opgegeven richting * sterkte
            rb.AddForce(Direction * ShootForce);

        }
    }
}
