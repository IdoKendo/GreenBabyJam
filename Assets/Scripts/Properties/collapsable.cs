using Shared.Enums;
using UnityEngine;

public class collapsable : MonoBehaviour {

    [SerializeField] internal float m_fallDelay = 0.5f;
    [SerializeField] private GameObject m_fallAnimation;

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
                Rigidbody2D animation_rigid = m_fallAnimation.GetComponent<Rigidbody2D>();
                if (!animation_rigid)
                {
                    animation_rigid = m_fallAnimation.AddComponent<Rigidbody2D>();
                    animation_rigid.gravityScale = 0.2f;
                }
                Rigidbody2D rigidBodyobject = GetComponent<Rigidbody2D>();
                rigidBodyobject.bodyType = RigidbodyType2D.Dynamic;
                rigidBodyobject.gravityScale = 0.2f;
                should_start_falling = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameCollisionType.PLAYER) && collision.otherCollider.GetType() == typeof(PolygonCollider2D))
        {
            Instantiate(m_fallAnimation, transform.position, Quaternion.identity);
            should_start_falling = true;
        }
    }

}
