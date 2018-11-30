using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private int m_health = 400;
    [SerializeField] private float m_jumpForce = 0.5f;

    private Rigidbody2D m_rigidBody;
    private bool m_isGrounded;

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
    }

    private void PerformAction()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            MeleeAttack();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            FireballAttack();
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            RaiseShield();
        }
    }

    private void MeleeAttack()
    {

    }

    private void FireballAttack()
    {

    }

    private void RaiseShield()
    {

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
}
