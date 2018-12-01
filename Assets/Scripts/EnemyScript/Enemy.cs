using Shared.Enums;
using UnityEngine;

public class Enemy : Creature
{
    [Header("Enemy")]
    [SerializeField] private bool m_smartEnemy = false;
    [SerializeField] private Transform m_platformDetector;
    [SerializeField] private Transform m_playerDetector;

    [Header("Smart Attributes")]
    [SerializeField] private float m_attackDistance = 0.1f;
    [SerializeField] private float m_attackCooldown = 1f;


    private float m_attackTime = 0;
    
    private void Update()
    {
        bool toPatrol = true;
        
        if (m_smartEnemy)
        {
            toPatrol = FindPlayer();

            if (m_attackTime > 0)
            {
                toPatrol = false;
                m_attackTime -= Time.deltaTime;
            }
        }

        if (toPatrol)
        {
            Patrol();
        }
    }

    private bool FindPlayer()
    {
        bool foundPlayer = true;
        Vector2 direction = m_direction == EDirection.Left ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(m_playerDetector.position, direction, m_attackDistance);

        Debug.DrawRay(m_playerDetector.position, direction, new Color(0, 0, 255));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(CharacterType.Player))
            {
                if (m_attackTime <= 0)
                {
                    m_attackTime = m_attackCooldown;
                    Attack();
                }

                foundPlayer = false;
            }
        }

        return foundPlayer;
    }

    private void Attack()
    {
        Debug.Log("Attacking player!");
        // TODO: Implement attacking the player
    }

    private void Patrol()
    {
        Vector2 direction = m_direction == EDirection.Left ? new Vector2(-1, -0.5f) : new Vector2(1, -0.5f);
        RaycastHit2D hit = Physics2D.Raycast(m_platformDetector.position, direction, 1f);

        Debug.DrawRay(m_platformDetector.position, direction, new Color(255, 0, 0));

        if (hit.collider == null)
        {
            if (m_direction == EDirection.Left)
            {
                m_direction = EDirection.Right;
            }
            else
            {
                m_direction = EDirection.Left;
            }

            transform.localRotation = Quaternion.Euler(0, m_direction == EDirection.Left ? 0 : 180, 0);
        }

        Move();
    }

    private void Move()
    {
        float delta = Time.deltaTime * m_moveSpeed;

        transform.position = new Vector2()
        {
            x = m_direction == EDirection.Left ? transform.position.x - delta : transform.position.x + delta,
            y = transform.position.y
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CollisionType.PlayerDamageDealer)
        {
            ProcessHit(collision.gameObject.GetComponent<DamageDealer>());
        }
    }
}
