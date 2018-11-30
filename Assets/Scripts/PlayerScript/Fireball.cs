using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : DamageDealer
{
    [Header("Movement")]
    [SerializeField] private float m_speed = 8f;
    [SerializeField] private int m_direction = 1;
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
        m_upSpeed -= 0.08f;

        Vector2 movement = new Vector2(m_direction, m_upSpeed);
        
        m_rigidBody.AddForce(movement * m_speed);
    }
}
