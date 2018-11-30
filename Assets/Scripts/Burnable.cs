using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Burn();
    }

    void Burn()
    {
        ParticleSystem fire = GetComponentInChildren<ParticleSystem>();
        fire.Play();
        Debug.Log("HELP I'M BURNING");
    }
}
