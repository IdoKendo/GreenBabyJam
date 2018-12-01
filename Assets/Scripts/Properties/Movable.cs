using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] private float m_distance = 8f;
    [SerializeField] private EDirection m_startDirection = EDirection.Left;
    [SerializeField] private float m_speed = 2f;

    private float m_startPositionX;
    private float m_flipPositionX;
    private EDirection m_currentDrection;

    private void Start()
    {
        m_currentDrection = m_startDirection;
        m_startPositionX = transform.position.x;
        m_flipPositionX = m_currentDrection == EDirection.Left ? m_startPositionX - m_distance : m_startPositionX + m_distance;
    }

    private void Update()
    {
        float delta = Time.deltaTime * m_speed;

        transform.position = new Vector2()
        {
            x = m_currentDrection == EDirection.Left ? transform.position.x - delta : transform.position.x + delta,
            y = transform.position.y
        };

        if (transform.position.x <= m_flipPositionX)
        {
            if (m_currentDrection == EDirection.Left)
            {
                m_currentDrection = EDirection.Right;
            }
            else
            {
                m_currentDrection = EDirection.Left;
            }
        }
        else if (transform.position.x >= m_startPositionX)
        {
            if (m_currentDrection == EDirection.Left)
            {
                m_currentDrection = EDirection.Right;
            }
            else
            {
                m_currentDrection = EDirection.Left;
            }
        }
    }
}
