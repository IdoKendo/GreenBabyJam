using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    [Header("Player")]
    [SerializeField] private float m_jumpForce = 0.5f;

    [Header("Weapons")]
    [SerializeField] private GameObject m_weaponPrefab;
    [SerializeField] private bool m_unlockedFireballs = false;
    [SerializeField] private GameObject m_fireballPrefab;
    [SerializeField] private bool m_unlockedShield = false;
    [SerializeField] private float m_superJumpModifier = 1.5f;

    [Header("Cooldowns")]
    [SerializeField] private float m_weaponCooldown = 1f;
    [SerializeField] private float m_fireballCooldown = 1f;

    private Rigidbody2D m_rigidBody;
    private bool m_isGrounded;
    private float m_weaponTime = 0f;
    private float m_fireballTime = 0f;
    private bool m_shielded = false;

    public float MaxHealth { get { return m_maxHealth; } }
    public float Health { get { return m_currHealth; } }
    public bool Fireballs { get { return m_unlockedFireballs; } }
    public bool Shield { get { return m_unlockedShield; } }

    private void Start()
    {
        m_currHealth = m_maxHealth;
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Move();
        Jump();
        PerformAction();
        Cooldown();
    }

    private void Move()
    {
        if (m_shielded)
        {
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
            return;
        }

        float deltaX = Input.GetAxis("Horizontal");

        m_rigidBody.velocity = new Vector2(deltaX * m_moveSpeed, m_rigidBody.velocity.y);
        transform.localRotation = Quaternion.Euler(0, (deltaX  < 0) ? 180 : 0, 0);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            Vector2 jumpVector = Vector2.up * m_jumpForce;

            if (m_shielded)
            {
                jumpVector *= m_superJumpModifier;
            }

            m_rigidBody.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    private void PerformAction()
    {
        if (Input.GetButtonDown("Fire1") && m_weaponTime <= 0)
        {
            MeleeAttack();
        }
        else if (Input.GetButtonDown("Fire2") && m_unlockedFireballs && m_fireballTime <= 0)
        {
            FireballAttack();
        }
        else if (m_unlockedShield)
        {
            if (Input.GetButton("Fire3"))
            {
                m_shielded = true;
            }
            else
            {
                m_shielded = false;
            }
        }
    }

    private void Cooldown()
    {
        if (m_weaponTime > 0)
        {
            m_weaponTime -= Time.deltaTime;
        }

        if (m_fireballTime > 0)
        {
            m_fireballTime -= Time.deltaTime;
        }
    }

    private void MeleeAttack()
    {
        Instantiate(m_weaponPrefab, transform.position, Quaternion.identity);
        m_weaponTime = m_weaponCooldown;
    }

    private void FireballAttack()
    {
        Instantiate(m_fireballPrefab, transform.position, Quaternion.identity);
        m_fireballTime = m_fireballCooldown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyDamageDealer")
        {
            if (m_shielded)
            {
                return;
            }

            ProcessHit(collision.gameObject.GetComponent<DamageDealer>());
        }
    }

    public void UnlockFireballs()
    {
        m_unlockedFireballs = true;
    }

    public void UnlockShield()
    {
        m_unlockedShield = true;
    }
}
