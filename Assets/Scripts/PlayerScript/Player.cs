using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private int m_health = 400;
    [SerializeField] private float m_jumpForce = 0.5f;

    [Header("Power Ups")]
    [SerializeField] private bool m_unlockedFireballs = false;
    [SerializeField] private bool m_unlockedShield = false;

    private Rigidbody2D m_rigidBody;
    private bool m_isGrounded;

    public int Health { get { return m_health; } }
    public bool Fireballs { get { return m_unlockedFireballs; } }
    public bool Shield { get { return m_unlockedShield; } }

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Move();
        PerformAction();
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            m_rigidBody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
        }

        m_rigidBody.velocity = new Vector2(deltaX * m_moveSpeed, m_rigidBody.velocity.y);
        transform.localRotation = Quaternion.Euler(0, (deltaX  < 0) ? 180 : 0, 0);
    }

    private void PerformAction()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            MeleeAttack();
        }
        else if (Input.GetButtonDown("Fire2") && m_unlockedFireballs)
        {
            FireballAttack();
        }
        else if (Input.GetButtonDown("Fire3") && m_unlockedShield)
        {
            RaiseShield();
        }
    }

    private void MeleeAttack()
    {
        Debug.Log("Swinging ma club");
    }

    private void FireballAttack()
    {
        Debug.Log("Firing ma lazer");
    }

    private void RaiseShield()
    {
        Debug.Log("Shields up!");
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

    public void UnlockFireballs()
    {
        m_unlockedFireballs = true;
    }

    public void UnlockShield()
    {
        m_unlockedShield = true;
    }
}
