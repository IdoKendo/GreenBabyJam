using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] internal bool m_triggerAnimation;
    [SerializeField] internal Collectable_types m_collectableType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")// only players can trigger 
        {
            if (m_triggerAnimation)
                CollectionAnimation();

            var player = collision.GetComponent<Player>();
            if (player!=null) {
                player.TriggerCollectItem(this.m_collectableType);
            }
            Destroy(gameObject);
        }
    }

    private void CollectionAnimation()
    {
    }
}
