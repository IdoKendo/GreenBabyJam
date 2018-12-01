using UnityEngine;

public class ClubSlash : DamageDealer
{
    [Header("Stats")]
    [SerializeField] private float m_lifetime = 0.75f;

    private void Start()
    {
        Destroy(gameObject, m_lifetime);
    }
}
