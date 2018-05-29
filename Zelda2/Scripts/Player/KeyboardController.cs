using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class KeyboardController : MonoBehaviour
{
    // Control current game state
    enum STATE
    {
        CLEAR,
        TALKING,
        PAUSE,
        KNOCKBACK
    }

    // Attributes
    // Components
    Rigidbody2D m_Rigidbody;

    // Movement
    float m_Speed;
    float m_SprintSpeed;
    Vector2 m_MovementForce;
    bool m_Sprinting;
    bool m_AbleToMove;
    STATE m_State;

    // Singleton
    DialogueManager m_DialogueManager;

    // Used to get reference to components attatched to object
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Used to initialize properties
    void Start()
    {
        m_State = STATE.CLEAR;
        m_Speed = 2;
        m_SprintSpeed = 5f;
        m_MovementForce = Vector2.zero;
        m_Sprinting = false;
        m_AbleToMove = true;
        m_DialogueManager = DialogueManager.Instance;
    }

    // Used to update physics
    void FixedUpdate()
    {
        // Check inputs
        CheckForInput();

        // Set state
        SetAbleToMove();

        // Check if can move
        if (m_State == STATE.CLEAR)
        {
            // Use movement input and Rigidbody to move object
            ApplyMovement();
        }
    }

    // Get ipput from keyboard motion controls
    void CheckForInput()
    {
        // Reset sprint
        m_Sprinting = false;

        // Get input form motion controls
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        // Check if we are holding sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Sprinting = true;
        }

        // Set Delta force
        m_MovementForce = new Vector2(hori, vert).normalized;

        // Input for action button
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckForInteraction();
        }
    }

    void CheckForInteraction()
    {
        if (m_State == STATE.CLEAR)
        {
            // Check direction we last faced
            float xDir = m_MovementForce.x * 2;
            float yDir = m_MovementForce.y * 2;


            // Create Point and check for colliders in radius around that point
            Vector2 point = new Vector2(transform.position.x + xDir, transform.position.y + yDir);
            Collider2D[] results = new Collider2D[10];
            int hits = Physics2D.OverlapCircleNonAlloc(point, 1, results);

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    if (results[i].gameObject.CompareTag("NPC"))
                    {
                        results[i].GetComponent<NPC>().TriggerDialogue();
                    }
                }
            }
        }
        else if (m_State == STATE.TALKING)
        {
            m_DialogueManager.DisplayNextSentence();
        }

    }

    // Add Force to rigidbody moving the object
    void ApplyMovement()
    {
        // Reset velocity before collecting new input
        m_Rigidbody.velocity = Vector2.zero;

        // Check if sprint is being held and select proper speed
        float speed = m_Sprinting ? m_SprintSpeed : m_Speed;

        // Move using direction defined in movement vector
        m_Rigidbody.AddForce(m_MovementForce * speed, ForceMode2D.Impulse);
    }

    // Set able to move depending on a series of checks
    void SetAbleToMove()
    {
        if (m_DialogueManager.Talking)
        {
            m_State = STATE.TALKING;
        }
        else
        {
            m_State = STATE.CLEAR;
        }
    }
}
