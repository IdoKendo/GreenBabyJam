using System;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [Header("General")]
    [SerializeField] [Range(0, 15)] private float m_distance = 8f;
    [SerializeField] private float m_speed = 2f;

    [Header("Direction")]
    [SerializeField] private bool m_moveHorizontal = true;
    [SerializeField] private EHorizontalDirection m_horizontalStartDirection = EHorizontalDirection.LEFT;
    [SerializeField] private bool m_moveVertical = false;
    [SerializeField] private EVerticalDirection m_verticalStartDirection = EVerticalDirection.UP;

    private float m_distancePassedX = 0f;
    private float m_distancePassedY = 0f;
    private Vector2 m_startPosition;
    private EHorizontalDirection m_horizontalCurrDirection;
    private EVerticalDirection m_verticalCurrDirection;

    private void Start()
    {
        m_startPosition = transform.position;

        m_horizontalCurrDirection = m_horizontalStartDirection;
        m_verticalCurrDirection = m_verticalStartDirection;
    }

    private void Update()
    {
        Move();

        if (Math.Abs(m_distancePassedY) >= m_distance && m_moveVertical)
        {
            FlipVertical();
            m_distancePassedY = 0f;
        }

        if (Math.Abs(m_distancePassedX) >= m_distance && m_moveHorizontal)
        {
            FlipHorizontal();
            m_distancePassedX = 0f;
        }
    }

    private void Move()
    {
        float delta = Time.deltaTime * m_speed;
        float newHorizontalPosition = transform.position.x;
        float newVerticalPosition = transform.position.y;

        if (m_moveHorizontal)
        {
            newHorizontalPosition = m_horizontalCurrDirection == EHorizontalDirection.LEFT ? transform.position.x - delta : transform.position.x + delta;
            m_distancePassedX += delta;
        }

        if (m_moveVertical)
        {
            newVerticalPosition = m_verticalCurrDirection == EVerticalDirection.UP ? transform.position.y + delta : transform.position.y - delta;
            m_distancePassedY += delta;
        }

        transform.position = new Vector2()
        {
            x = newHorizontalPosition,
            y = newVerticalPosition
        };
    }

    private void FlipVertical()
    {
        if (m_verticalCurrDirection == EVerticalDirection.UP)
        {
            m_verticalCurrDirection = EVerticalDirection.DOWN;
        }
        else
        {
            m_verticalCurrDirection = EVerticalDirection.UP;
        }
    }

    private void FlipHorizontal()
    {
        if (m_horizontalCurrDirection == EHorizontalDirection.LEFT)
        {
            m_horizontalCurrDirection = EHorizontalDirection.RIGHT;
        }
        else
        {
            m_horizontalCurrDirection = EHorizontalDirection.LEFT;
        }
    }
}
