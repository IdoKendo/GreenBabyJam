using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name.ToLower().StartsWith("fireball"))
            Burn();
    }

    void Burn()
    {
        ParticleSystem fire = GetComponentInChildren<ParticleSystem>();
        fire.Play();
        SpawnEffect dissolve = GetComponent<SpawnEffect>();
        dissolve.Burn();
        Debug.Log("HELP I'M BURNING");
    }
    public void Destroy()
    {
        ParticleSystem fire = GetComponentInChildren<ParticleSystem>();
        fire.Stop();
        // TODO: Send to storytelling engine a message
    }
}
