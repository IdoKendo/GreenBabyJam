using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] private int m_maxHealth = 100;
    [SerializeField] private float m_speed = 0.5f;
    [SerializeField] private bool m_smartEnemy = false;
    [SerializeField] private Transform m_platformDetector;
    [SerializeField] private Transform m_playerDetector;

    [Header("Smart Attributes")]
    [SerializeField] private float m_attackDistance = 0.01f;
    [SerializeField] private float m_attackCooldown = 1f;

    [Header("Death Effects")]
    [SerializeField] private AudioClip m_deathSound;
    [SerializeField] [Range(0, 1)] private float m_deathVolume = 0.75f;

    private float m_currHealth;
    private bool m_facingLeft = true;
    private float m_attackTime = 0;

    private void Start()
    {
        m_currHealth = m_maxHealth;
    }
    
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
        Vector2 direction = m_facingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(m_playerDetector.position, direction, m_attackDistance);

        Debug.DrawRay(m_playerDetector.position, direction, new Color(0, 0, 255));

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
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
        Vector2 direction = m_facingLeft ? new Vector2(-1, -0.5f) : new Vector2(1, -0.5f);
        RaycastHit2D hit = Physics2D.Raycast(m_platformDetector.position, direction, 1f);

        Debug.DrawRay(m_platformDetector.position, direction, new Color(255, 0, 0));

        if (hit.collider == null)
        {
            m_facingLeft = !m_facingLeft;

            transform.localRotation = Quaternion.Euler(0, m_facingLeft ? 0 : 180, 0);
        }

        Move();
    }

    private void Move()
    {
        float delta = Time.deltaTime * m_speed;

        transform.position = new Vector2()
        {
            x = m_facingLeft ? transform.position.x - delta : transform.position.x + delta,
            y = transform.position.y
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerDamageDealer")
        {
            ProcessHit(collision.gameObject.GetComponent<DamageDealer>());
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
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
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(m_deathSound, Camera.main.transform.position, m_deathVolume);
    }
}
