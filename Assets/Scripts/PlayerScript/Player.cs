﻿using Assets.Scripts.Shared.Enums;
using Assets.Scripts.Shared.Managers;
using Shared.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject[] m_deathAnnimationArray;

    private Animator m_barbarianAnimator;
    private CapsuleCollider2D m_collider;
    private bool m_isGrounded;
    private bool m_isKnockedBack = false;
    private float m_fireballTime = 0f;
    private float m_knockbackTime = 0f;
    private SceneType m_Scene;
    private bool m_shielded = false;
    private float m_weaponTime = 0f;

    public float MaxHealth { get { return m_maxHealth; } }
    public float Health { get { return m_currHealth; } }
    public bool Fireballs { get { return m_unlockedFireballs; } }
    public bool Shield { get { return m_unlockedShield; } }

    private new void Start()
    {
        base.Start();
        m_barbarianAnimator = GetComponentInChildren<Animator>();
        m_collider = gameObject.GetComponent<CapsuleCollider2D>();

        m_maxHealth = Game_Manager.playerManager.MaxHealth;
        m_currHealth = Game_Manager.playerManager.Health;
        m_Scene = Game_Manager.playerManager.Scene;
        m_unlockedFireballs = Game_Manager.playerManager.Fireballs;
        m_unlockedShield = Game_Manager.playerManager.Shield;
    }

    private void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
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

        float deltaX = Input.GetAxis(AxisActionType.HORIZONTAL);
        m_barbarianAnimator.SetFloat("MoveSpeed", Mathf.Abs(deltaX));

        if (deltaX < 0)
        {
            m_direction = EHorizontalDirection.RIGHT;
        }
        else if (deltaX > 0)
        {
            m_direction = EHorizontalDirection.LEFT;
        }

        m_rigidBody.velocity = new Vector2(deltaX * m_moveSpeed, m_rigidBody.velocity.y);
        transform.localRotation = Quaternion.Euler(0, m_direction == EHorizontalDirection.LEFT ? 0 : 180, 0);
    }

    private void Jump()
    {
        if (Input.GetButtonDown(InputButtonType.JUMP) && m_isGrounded)
        {
            Sound_Manager.instance.Jump();
            Vector2 jumpVector = Vector2.up * m_jumpForce;
            m_barbarianAnimator.SetTrigger("Jump");

            if (m_shielded)
            {
                float potentialNewHealth = m_currHealth - m_superJumpSelfDamage;

                m_currHealth = Mathf.Clamp(potentialNewHealth, 1, potentialNewHealth);

                 jumpVector *= m_superJumpModifier;
            }

            m_rigidBody.AddForce(jumpVector, ForceMode2D.Impulse);
        }
        m_isGrounded = false;
    }

    private void PerformAction()
    {
        if (Input.GetButtonDown(InputButtonType.FIRE1) && m_weaponTime <= 0)
        {
            MeleeAttack();
        }
        else if (Input.GetButtonDown(InputButtonType.FIRE2) && m_unlockedFireballs && m_fireballTime <= 0)
        {
            FireballAttack();
        }
        else if (m_unlockedShield)
        {
            if (Input.GetButton(InputButtonType.FIRE3))
            {
                m_shielded = true;
                m_barbarianAnimator.SetBool("Blocking", true);
            }
            else
            {
                m_shielded = false;
                m_barbarianAnimator.SetBool("Blocking", false);
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
        m_barbarianAnimator.SetLayerWeight(1, 1);
        m_barbarianAnimator.SetTrigger("SimpleAttack");
        Instantiate(m_weaponPrefab, transform.position, Quaternion.identity);
        m_weaponTime = m_weaponCooldown;
    }

    private void FireballAttack()
    {
        m_barbarianAnimator.SetLayerWeight(1, 1);
        m_barbarianAnimator.SetTrigger("FireAttack");
        GameObject fireball = Instantiate(m_fireballPrefab, transform.position, Quaternion.identity);
        float potentialNewHealth = m_currHealth - m_fireballSelfDamage;

        fireball.GetComponent<Fireball>().SetDirection(m_direction);
        m_fireballTime = m_fireballCooldown;
        m_currHealth = Mathf.Clamp(potentialNewHealth, 1, potentialNewHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameCollisionType.GROUND))
        {
            for (int i = 0; i < collision.contactCount; i++)
                if (Mathf.Abs(collision.contacts[i].normal[1]) > 0.9f)
                    m_isGrounded = true;
        }
        else if (collision.gameObject.CompareTag(GameCollisionType.PLATFORM))
        {
            m_isGrounded = true;
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameCollisionType.GROUND))
        {
            m_isGrounded = false;
        }
        else if (collision.gameObject.CompareTag(GameCollisionType.PLATFORM))
        {
            m_isGrounded = false;
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == CollisionType.ENEMY_DAMAGE_DEALER)
        {
            if (m_shielded)
                return;

            Knockback();
            ProcessHit(collision.gameObject.GetComponent<DamageDealer>());
            return;
        }

        if (collision.tag == CollisionType.OPEN_DOOR_ROOM2)
        {

        }
    }

    private void CheckIfGrounded()
    {
        if (m_isGrounded)
            return;
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int colNum = m_collider.GetContacts(contacts);
        for (int i = 0; i < colNum; i++)
        {
            if (contacts[i].normal[1] > 0.9f)
                m_isGrounded = true;
        }
    }

    private void Knockback()
    {
        float knockbackPower = m_knockbackPower;

        if (m_direction == EHorizontalDirection.LEFT)
        {
            knockbackPower = -knockbackPower;

        }
        m_isKnockedBack = true;
        m_knockbackTime = m_knockbckDuration;
        m_rigidBody.velocity = new Vector2(knockbackPower, knockbackPower);
    }

    protected override void DieAnimation()
    {
        Sound_Manager.instance.Boom();
        foreach (GameObject animationPrefab in m_deathAnnimationArray)
        {
            Instantiate(animationPrefab, transform.position, Quaternion.identity);
        }
        
        m_currHealth = m_maxHealth;
        Save(m_Scene);
        SceneManager.LoadScene((int)m_Scene);
    }
    public void TriggerCollectItem(Collectable_types item)
    {
        switch (item)
        {
            case Collectable_types.Fireball:
                m_unlockedFireballs = true;
                break;
            case Collectable_types.Shield:
                m_unlockedShield = true;
                break;
            default:
                break;
        }
    }


    //Reset animation layer weight
    void ResetAnimationLayer()
    {
        m_barbarianAnimator.SetLayerWeight(1, 0);
    }

    public void Save(SceneType scene)
    {
        Game_Manager.playerManager.Health = m_currHealth;
        Game_Manager.playerManager.Fireballs = m_unlockedFireballs;
        Game_Manager.playerManager.MaxHealth = m_maxHealth;
        Game_Manager.playerManager.Scene = scene;
        Game_Manager.playerManager.Shield = m_unlockedShield;
    }
}
