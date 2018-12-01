using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private Transform bar;
   
    private Player player;

    

    // Use this for initialization
    void Start () {
        bar = transform.Find("Bar");
        player = FindObjectOfType<Player>();
        
    }
	
	// Update is called once per frame
	void Update () {
        

        //if (player != null)
        //{
            float normilizedHealth = player.Health / player.MaxHealth;
            bar.localScale = new Vector2(normilizedHealth, 1f);
        //}


    }
    
}
