using UnityEngine;

public class Fireball : DamageDealer
{
    [Header("Movement")]
    [SerializeField] private float m_speed = 8f;
    [SerializeField] private EDirection m_direction = EDirection.Right;
    [SerializeField] private float m_upSpeed = 2;

    [Header("Stats")]
    [SerializeField] private float m_lifetime = 5f;
    
    private Rigidbody2D m_rigidBody;

    private void Start()
    {
        Destroy(gameObject, m_lifetime);

        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float xVector = Vector2.left.x;

        m_upSpeed -= 0.08f;

        if (m_direction == EDirection.Left)
        {
            xVector = Vector2.right.x;
        }

        Vector2 movement = new Vector2(xVector, m_upSpeed);
        
        m_rigidBody.AddForce(movement * m_speed);
    }

    public void SetDirection(EDirection direction)
    {
        m_direction = direction;
    }
}
