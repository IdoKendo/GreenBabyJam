using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private int m_health = 400;

    private void Start()
    {
        
    }
    
    private void Update()
    {
        Move();
        Jump();
        PerformAction();
    }

    private void Move()
    {

    }

    private void Jump()
    {

    }

    private void PerformAction()
    {

    }

    private void MeleeAttack()
    {

    }

    public void FireballAttack()
    {

    }

    public void RaiseShield()
    {

    }

    public void SuperJump()
    {

    }
}
