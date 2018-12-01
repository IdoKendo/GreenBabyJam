using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Creature")]
    [SerializeField] internal float m_maxHealth;
    [SerializeField] internal float m_moveSpeed;

    internal float m_currHealth;

    internal void ProcessHit(DamageDealer damageDealer)
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
        DieAnimation();
        Destroy(gameObject);
    }

    protected virtual void DieAnimation()
    {
        Debug.Log("Bad Animation");

    }
}
