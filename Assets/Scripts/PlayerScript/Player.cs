﻿using Shared.Enums;
using UnityEngine;


public enum Collectable_types {
    Fireball,
    Shield
}
public class Player : Creature
{
    [Header("Player")]
    [SerializeField] private float m_jumpForce = 5f;
    [SerializeField] private float m_knockbackPower = 3f;
    [SerializeField] private float m_knockbckDuration = 0.5f;

    [Header("Weapons")]
    [SerializeField] private GameObject m_weaponPrefab;
    [SerializeField] private bool m_unlockedFireballs = false;
    [SerializeField] private float m_fireballSelfDamage = 20f;
    [SerializeField] private GameObject m_fireballPrefab;
    [SerializeField] private bool m_unlockedShield = false;
    [SerializeField] private float m_superJumpModifier = 1.5f;
    [SerializeField] private float m_superJumpSelfDamage = 20f;

    [Header("Cooldowns")]
    [SerializeField] private float m_weaponCooldown = 1f;
    [SerializeField] private float m_fireballCooldown = 1f;

    [Header("Death Animation")]
    private Animator m_barbarian_animator;
    [SerializeField] private GameObject[] m_deathAnnimationArray;

    private bool m_isGrounded;
    private bool m_isKnockedBack = false;
    private float m_weaponTime = 0f;
    private float m_fireballTime = 0f;
    private float m_knockbackTime = 0f;
    private bool m_shielded = false;

    public float MaxHealth { get { return m_maxHealth; } }
    public float Health { get { return m_currHealth; } }
    public bool Fireballs { get { return m_unlockedFireballs; } }
    public bool Shield { get { return m_unlockedShield; } }

    private new void Start()
    {
        base.Start();
        m_barbarian_animator = GetComponentInChildren<Animator>();
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

        if (m_isKnockedBack)
        {
            return;
        }

        float deltaX = Input.GetAxis(AxisActionType.Horizontal);
        m_barbarian_animator.SetFloat("MoveSpeed", Mathf.Abs(deltaX));

        if (deltaX < 0)
        {
            m_direction = EDirection.Right;
        }
        else if (deltaX > 0)
        {
            m_direction = EDirection.Left;
        }

        m_rigidBody.velocity = new Vector2(deltaX * m_moveSpeed, m_rigidBody.velocity.y);
        transform.localRotation = Quaternion.Euler(0, m_direction == EDirection.Left ? 0 : 180, 0);
    }

    private void Jump()
    {
        if (Input.GetButtonDown(InputButtonType.Jump) && m_isGrounded)
        {
            Vector2 jumpVector = Vector2.up * m_jumpForce;

            if (m_shielded)
            {
                float potentialNewHealth = m_currHealth - m_superJumpSelfDamage;

                m_currHealth = Mathf.Clamp(potentialNewHealth, 1, potentialNewHealth);
                jumpVector *= m_superJumpModifier;
            }

            m_rigidBody.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    private void PerformAction()
    {
        if (Input.GetButtonDown(InputButtonType.Fire1) && m_weaponTime <= 0)
        {
            MeleeAttack();
        }
        else if (Input.GetButtonDown(InputButtonType.Fire2) && m_unlockedFireballs && m_fireballTime <= 0)
        {
            FireballAttack();
        }
        else if (m_unlockedShield)
        {
            if (Input.GetButton(InputButtonType.Fire3))
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

        if (m_isKnockedBack)
        {
            m_knockbackTime -= Time.deltaTime;

            if (m_knockbackTime <= 0)
            {
                m_isKnockedBack = false;
            }
        }
    }

    private void MeleeAttack()
    {
        Instantiate(m_weaponPrefab, transform.position, Quaternion.identity);
        m_weaponTime = m_weaponCooldown;
    }

    private void FireballAttack()
    {
        GameObject fireball = Instantiate(m_fireballPrefab, transform.position, Quaternion.identity);
        float potentialNewHealth = m_currHealth - m_fireballSelfDamage;

        fireball.GetComponent<Fireball>().SetDirection(m_direction);
        m_fireballTime = m_fireballCooldown;
        m_currHealth = Mathf.Clamp(potentialNewHealth, 1, potentialNewHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameCollisionType.Ground))
        {
            m_isGrounded = true;
        }
        else if (collision.gameObject.CompareTag(GameCollisionType.Platform))
        {
            m_isGrounded = true;
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameCollisionType.Ground))
        {
            m_isGrounded = false;
        }
        else if (collision.gameObject.CompareTag(GameCollisionType.Platform))
        {
            m_isGrounded = false;
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CollisionType.EnemyDamageDealer)
        {
            if (m_shielded)
            {
                return;
            }

            Knockback();
            ProcessHit(collision.gameObject.GetComponent<DamageDealer>());
        }
    }

    private void Knockback()
    {
        float knockbackPower = m_knockbackPower;

        if (m_direction == EDirection.Left)
        {
            knockbackPower = -knockbackPower;

        }
        m_isKnockedBack = true;
        m_knockbackTime = m_knockbckDuration;
        m_rigidBody.velocity = new Vector2(knockbackPower, knockbackPower);
    }

    public void UnlockFireballs()
    {
        m_unlockedFireballs = true;
    }

    public void UnlockShield()
    {
        m_unlockedShield = true;
    }

    protected override void DieAnimation()
    {
        foreach (GameObject animationPrefab in m_deathAnnimationArray)
        {
            Instantiate(animationPrefab, transform.position, Quaternion.identity);
        }
    }
    public void Trigger_collect_item(Collectable_types item)
    {
        switch (item)
        {
            case Collectable_types.Fireball:
                this.UnlockFireballs();
                break;
            case Collectable_types.Shield:
                break;
            default:
                break;
        }
    }
}
