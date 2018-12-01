using UnityEngine;

public class Movable : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float m_distance = 8f;
    [SerializeField] private float m_speed = 2f;

    [Header("Direction")]
    [SerializeField] private bool m_moveHorizontal = true;
    [SerializeField] private eHorizontalDirection m_horizontalStartDirection = eHorizontalDirection.Left;
    [SerializeField] private bool m_moveVertical = false;
    [SerializeField] private eVerticalDirection m_verticalStartDirection = eVerticalDirection.Up;

    private float m_startPositionX;
    private float m_flipPositionX;
    private float m_startPositionY;
    private float m_flipPositionY;
    private eHorizontalDirection m_horizontalCurrDirection;
    private eVerticalDirection m_verticalCurrDirection;

    private void Start()
    {
        m_horizontalCurrDirection = m_horizontalStartDirection;
        m_verticalCurrDirection = m_verticalStartDirection;
        m_startPositionX = transform.position.x;
        m_flipPositionX = m_horizontalCurrDirection == eHorizontalDirection.Left ? m_startPositionX - m_distance : m_startPositionX + m_distance;
        m_startPositionY = transform.position.y;
        m_flipPositionY = m_verticalCurrDirection == eVerticalDirection.Up ? m_startPositionY - m_distance : m_startPositionY + m_distance;
    }

    private void Update()
    {
        Move();
        FlipVertical();
        FlipHorizontal();
    }

    private void Move()
    {
        float delta = Time.deltaTime * m_speed;
        float newHorizontalPosition = transform.position.x;
        float newVerticalPosition = transform.position.y;

        if (m_moveHorizontal)
        {
            newHorizontalPosition = m_horizontalCurrDirection == eHorizontalDirection.Left ? transform.position.x - delta : transform.position.x + delta;
        }

        if (m_moveVertical)
        {
            newVerticalPosition = m_verticalCurrDirection == eVerticalDirection.Up ? transform.position.y - delta : transform.position.y + delta;
        }

        transform.position = new Vector2()
        {
            x = newHorizontalPosition,
            y = newVerticalPosition
        };
    }

    private void FlipVertical()
    {
        if (transform.position.y <= m_flipPositionY)
        {
            if (m_verticalCurrDirection == eVerticalDirection.Up)
            {
                m_verticalCurrDirection = eVerticalDirection.Down;
            }
            else
            {
                m_verticalCurrDirection = eVerticalDirection.Up;
            }
        }
        else if (transform.position.y >= m_startPositionY)
        {
            if (m_verticalCurrDirection == eVerticalDirection.Up)
            {
                m_verticalCurrDirection = eVerticalDirection.Down;
            }
            else
            {
                m_verticalCurrDirection = eVerticalDirection.Up;
            }
        }
    }

    private void FlipHorizontal()
    {
        if (transform.position.x <= m_flipPositionX)
        {
            if (m_horizontalCurrDirection == eHorizontalDirection.Left)
            {
                m_horizontalCurrDirection = eHorizontalDirection.Right;
            }
            else
            {
                m_horizontalCurrDirection = eHorizontalDirection.Left;
            }
        }
        else if (transform.position.x >= m_startPositionX)
        {
            if (m_horizontalCurrDirection == eHorizontalDirection.Left)
            {
                m_horizontalCurrDirection = eHorizontalDirection.Right;
            }
            else
            {
                m_horizontalCurrDirection = eHorizontalDirection.Left;
            }
        }
    }
}
