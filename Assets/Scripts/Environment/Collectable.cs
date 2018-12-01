using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    [SerializeField] internal bool trigger_animation;
    [SerializeField] internal Collectable_types collectable_type;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")// only players can trigger 
        {
            if (trigger_animation)
                CollectionAnimation();

            var player = collision.GetComponent<Player>();
            if (player!=null) {
                player.Trigger_collect_item(this.collectable_type);
            }
            Destroy(gameObject);
        }
    }

    private void CollectionAnimation()
    {
    }
}
