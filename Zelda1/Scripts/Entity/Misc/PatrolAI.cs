using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class PatrolAI : MonoBehaviour
{
    // Attributes
    Rigidbody2D m_RGB;
    float m_Time;
    bool m_ResettingPosition;
    bool m_MovingRight;
    Vector3 m_StartPos;

    public bool ResettingPosition
    {
        set
        {
            m_ResettingPosition = value;
        }
    }

    // Used to get reference to components attatched to game object
    void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();
    }

    // Used to initialized
    void Start()
    {
        m_Time = 5;
        m_ResettingPosition = true;
        m_StartPos = transform.position;
        m_MovingRight = true;
    }

    public void StartPatrol(float speed)
    {
        if (m_ResettingPosition)
        {
            float dist = Vector3.Distance(transform.position, m_StartPos);
            if (dist > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_StartPos, speed * Time.deltaTime);
                Vector3 direction = m_StartPos - transform.position;
            }
            else
            {
                m_ResettingPosition = false;
            }
        }
        else
        {
            HorizontalPatrol(speed);
        }
    }

    // Move from left to right
    void HorizontalPatrol(float speed)
    {
        if (m_MovingRight)
        {
            m_RGB.velocity = Vector2.right * speed;
            if (transform.position.x > m_StartPos.x + 5)
            {
                m_MovingRight = false;
            }
        }
        else
        {
            m_RGB.velocity = Vector2.left * speed;
            if (transform.position.x < m_StartPos.x - 5)
            {
                m_MovingRight = true;
            }
        }
    }

    // Move up and down
    public void VerticalPatrol(float speed)
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, m_Time));
    }


}
