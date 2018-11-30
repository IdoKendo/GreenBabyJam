using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage Dealer Values")]
    [SerializeField] private float m_damage = 50;

    public float Damage { get { return m_damage; } }
}
