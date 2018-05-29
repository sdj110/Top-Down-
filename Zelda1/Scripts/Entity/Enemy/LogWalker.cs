using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class LogWalker : Entity
{
    enum STATE
    {
        IDLE,
        MOVING,
        SLEEPING,
        ATTACKING,
        HIT
    }


    // Attributes
    // Required
    Rigidbody2D m_RGB;
    Animator m_Animator;
    SpriteRenderer m_SpriteRenderer;

    // Scripts
    PatrolAI m_PatrolScript;
    FlashObjectAlpha m_FlashScript;
    LogWalkerAttack m_AttackScript;

    float m_IdleTime;
    float m_MaxIdleTime;
    float m_KnockbackTime;

    Vector3 m_DirectionVector;
    STATE m_State;

    // Properties
    public float Speed
    {
        get
        {
            return m_Speed;
        }
    }

    // Use awake to get component attatched to object
    void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_PatrolScript = GetComponent<PatrolAI>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_AttackScript = GetComponentInChildren<LogWalkerAttack>();
        m_FlashScript = GetComponent<FlashObjectAlpha>();
    }

    // Used for initialization
    void Start()
    {
        m_MaxIdleTime = 5;
        m_IdleTime = m_MaxIdleTime;
        m_Speed = 1f;
        m_Damage = 1;
        m_Health = 3;
        m_KnockbackTime = 0f;
        m_State = STATE.SLEEPING;
    }

    // Runs once a frame
    void Update()
    {
        if (m_KnockbackTime <= 0)
        {
            // Check which state log is in
            if (m_State == STATE.IDLE)
            {
                CountIdleTime();
            }
            else if (m_State == STATE.SLEEPING)
            {

            }
            else if (m_State == STATE.MOVING)
            {
                m_RGB.velocity = Vector2.zero;

                if (!m_AttackScript.PlayerInRange)
                {
                    m_PatrolScript.StartPatrol(m_Speed);
                }
                else
                {

                }
            }
        }
        else
        {
            m_KnockbackTime -= Time.deltaTime;
        }

        // Update Animation controller after all input is processed
        UpdateAnimationController();
    }

    // Count idle time and check when log should fall asleep
    void CountIdleTime()
    {
        if (m_IdleTime >= 0)
        {
            m_IdleTime -= Time.deltaTime;
        }
        else
        {
            m_State = STATE.SLEEPING;
            m_IdleTime = m_MaxIdleTime;
        }
    }

    // Update the Animation controller
    void UpdateAnimationController()
    {
        m_Animator.SetInteger("State", (int)m_State);
        if (m_KnockbackTime <= 0)
        {
            m_Animator.SetFloat("Hori", m_RGB.velocity.x);
            m_Animator.SetFloat("Vert", m_RGB.velocity.y);
        }
        else
        {
            m_Animator.SetFloat("Hori", -m_RGB.velocity.x);
            m_Animator.SetFloat("Vert", -m_RGB.velocity.y);
        }
    }

    // Runs when object enters trigger collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Get object we collided with
        GameObject obj = collision.gameObject;
        
        // Check if we collide with player
        if (obj.CompareTag("Player"))
        {
            m_State = STATE.MOVING;
        }
    }

    // Check if we hit the player with our collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            float time = 0.25f;
            obj.GetComponent<PlayerController>().ApplyKnockback(time, 5, m_Damage);
        }
    }

    // Runs when object is hit with player weapon
    public void TakeDamage(Vector3 hitPosition, float duration, float power)
    {
        m_KnockbackTime = duration;
        Vector3 vec = transform.position - hitPosition;
        m_RGB.AddForce(vec * power, ForceMode2D.Impulse);
        StartCoroutine(m_FlashScript.FlashAlpha());
    }


}


