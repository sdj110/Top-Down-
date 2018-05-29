using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogWalkerAttack : MonoBehaviour
{
    // Attributes
    private Rigidbody2D m_RGB;
    private bool m_PlayerInRange;
    private bool m_CanAttack;
    private float m_AttackSpeed;
    private Vector3 m_Target;
    private Vector3 m_StartPosition;

    // Properties
    public bool PlayerInRange
    {
        get
        {
            return m_PlayerInRange;
        }
        set
        {
            m_PlayerInRange = value;
        }
    }
    public Vector3 Target
    {
        get
        {
            return m_Target;
        }

        set
        {
            m_Target = value;
        }
    }
    public Vector3 StartPosition
    {
        get
        {
            return m_StartPosition;
        }

        set
        {
            m_StartPosition = value;
        }
    }

    // Used to get references to components on object
    void Awake()
    {
        m_RGB = GetComponentInParent<Rigidbody2D>();
    }

    // used to Initialize attributes
    void Start()
    {
        m_CanAttack = true;
        m_PlayerInRange = false;
        m_AttackSpeed = 1;
    }

    // Runs once a frame. Will run when logwalker is attacking
    void FixedUpdate()
    {
        if (m_PlayerInRange)
        {
            float dist = Vector3.Distance(m_Target, transform.position);
            if (dist > 0.5f && m_CanAttack)
            {
                Vector2 force = m_Target - m_StartPosition;
                m_RGB.AddForce(force * m_AttackSpeed, ForceMode2D.Impulse);
            }
            else
            {
                float return_distance = Vector3.Distance(m_StartPosition, transform.position);
                if (return_distance > 0.5f)
                {
                    Vector2 force = m_StartPosition - m_Target;
                    m_RGB.AddForce(force * m_AttackSpeed, ForceMode2D.Impulse);
                    m_CanAttack = false;
                }
                else
                {
                    Invoke("ResetCanAttack", 2f);
                }
            }
        }
    }

    // Reset can attack boolian
    void ResetCanAttack()
    {
        m_CanAttack = true;
    }

    // Runs when object enters trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            m_PlayerInRange = true;
            m_Target = obj.transform.position;
            m_StartPosition = transform.position;
        }
    }

    // Runs when object exits the trigger
    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            m_PlayerInRange = false;
        }
    }

}
