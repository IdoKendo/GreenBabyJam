using Shared.Enums;
using UnityEngine;

public class Enemy : Creature
{
    [Header("Enemy")]
    [SerializeField] private Transform m_platformDetector;
    [SerializeField] private Transform m_playerDetector;
    [SerializeField] private float m_distanceToDetect = 1.5f;
    [SerializeField] private bool m_smartEnemy = false;

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
        Vector2 direction = m_direction == EHorizontalDirection.LEFT ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(m_playerDetector.position, direction, m_attackDistance);

        Debug.DrawRay(m_playerDetector.position, direction, new Color(0, 0, 255));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(CharacterType.PLAYER))
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
        Vector2 directionDown = m_direction == EHorizontalDirection.LEFT ? new Vector2(-1, -0.5f) : new Vector2(1, -0.5f);
        Vector2 directionForward = m_direction == EHorizontalDirection.LEFT ? Vector2.left : Vector2.right;
        RaycastHit2D hitDown = Physics2D.Raycast(m_platformDetector.position, directionDown, m_distanceToDetect);
        RaycastHit2D hitForward = Physics2D.Raycast(m_platformDetector.position, directionForward, m_distanceToDetect);

        Debug.DrawRay(m_platformDetector.position, directionDown, new Color(255, 0, 0));
        Debug.DrawRay(m_platformDetector.position, directionForward, new Color(255, 0, 0));

        if (hitDown.collider == null)
        {
            ChangeDirection();
        }
        if (hitForward.collider != null)
        {
            if (hitForward.collider.tag == "Ground" || hitForward.collider.tag == "Enemy")
                ChangeDirection();
        }

        Move();
    }

    private void ChangeDirection()
    {
        if (m_direction == EHorizontalDirection.LEFT)
        {
            m_direction = EHorizontalDirection.RIGHT;
        }
        else
        {
            m_direction = EHorizontalDirection.LEFT;
        }

        transform.localRotation = Quaternion.Euler(0, m_direction == EHorizontalDirection.LEFT ? 0 : 180, 0);
    }

    private void Move()
    {
        float delta = Time.deltaTime * m_moveSpeed;

        transform.position = new Vector2()
        {
            x = m_direction == EHorizontalDirection.LEFT ? transform.position.x - delta : transform.position.x + delta,
            y = transform.position.y
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CollisionType.PLAYER_DAMAGE_DEALER)
        {
            ProcessHit(collision.gameObject.GetComponent<DamageDealer>());
        }
    }
}
