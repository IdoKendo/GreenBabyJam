using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] private float m_maxHealth = 100f;
    [SerializeField] private float m_speed = 1f;
    [SerializeField] private Transform m_face;

    [Header("Death Effects")]
    [SerializeField] private AudioClip m_deathSound;
    [SerializeField] [Range(0, 1)] private float m_deathVolume = 0.75f;

    private float m_currHealth;
    private bool m_facingLeft = true;

    private void Start()
    {
        m_currHealth = m_maxHealth;
    }
    
    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector2 direction = m_facingLeft ? new Vector2(-1, -0.5f) : new Vector2(1, -0.5f);
        RaycastHit2D hit = Physics2D.Raycast(m_face.position, direction, 1f);

        Debug.DrawRay(m_face.position, direction);

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
        //AudioSource.PlayClipAtPoint(m_deathSound, Camera.main.transform.position, m_deathVolume);
    }
}
