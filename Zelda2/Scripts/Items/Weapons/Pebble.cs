using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pebble : MonoBehaviour
{
    // Attributes
    [SerializeField] float m_Speed;

    //Components
    Rigidbody2D m_RGB;
    public Vector2 direction = Vector2.up;
    

    // Used to get reference to component attatched to object
    void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();
    }

    // Initialize
    void Start()
    {
        m_Speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset velocity vector
        m_RGB.velocity = Vector2.zero;

        // Calculate direction force vector
        Vector2 force = direction * m_Speed;

        // Rotate projectile 10 degrees a frame
        transform.Rotate(0, 0, 10);

        // Add force to rigidbody
        m_RGB.AddForce(force, ForceMode2D.Impulse);
    }

    // Check for collision
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Remove object from game on collision
        Destroy(gameObject);
    }
}