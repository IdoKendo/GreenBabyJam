using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collapsable : MonoBehaviour {

    [SerializeField] internal float m_fallDelay = 0.5f;

    private bool should_start_falling;
    float timer = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (should_start_falling)
        {
            if (timer < m_fallDelay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Rigidbody2D rigidBodyobject = GetComponent<Rigidbody2D>();
                rigidBodyobject.bodyType = RigidbodyType2D.Dynamic;
                rigidBodyobject.gravityScale = 0.2f;
                should_start_falling = false;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            should_start_falling = true;
        }
    }

}
