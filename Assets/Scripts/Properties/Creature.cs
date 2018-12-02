using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Creature")]
    [SerializeField] internal float m_maxHealth;
    [SerializeField] internal float m_moveSpeed;

    internal float m_currHealth;
    internal Rigidbody2D m_rigidBody;
    internal EHorizontalDirection m_direction = EHorizontalDirection.LEFT;

    internal void Start()
    {
        m_currHealth = m_maxHealth;
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    internal void ProcessHit(DamageDealer damageDealer)
    {
        m_currHealth -= damageDealer.Damage;

        Debug.Log(string.Format("I got hit! my hp is: {0}", m_currHealth));

        if (m_currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DieAnimation();
        Destroy(gameObject);
    }

    protected virtual void DieAnimation()
    {
        Debug.Log("Bad Animation");

    }
}
