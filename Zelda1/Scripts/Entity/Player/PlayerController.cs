using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    // Attributes
    // Required 
    Rigidbody2D m_RGB;
    Animator m_Animator;
    BoxCollider2D m_Collider;
    SpriteRenderer m_SpriteRenderer;

    // Serialized
    [SerializeField] GameObject m_SprintObject;

    // Motion Vectors
    Vector2 m_DeltaForce;
    Vector2 m_LastDirection;
    
    // Scripts
    Player m_PlayerScript;
    FlashObjectAlpha m_FlashScript;

    // State Bools
    bool m_IsMoving;
    bool m_IsAttacking;
    bool m_IsBlocking;
    bool m_Sprinting;


    float m_AttackRadius;
    float m_AttackDist;
    float m_KnockBackTime;
    float m_SprintAnimationDelay;


    // Used to get reference to object's components
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_RGB = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_PlayerScript = GetComponent<Player>();
        m_FlashScript = GetComponent<FlashObjectAlpha>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Used to set attributes and references to other objects
    void Start()
    {
        m_DeltaForce = Vector2.zero;
        m_RGB.gravityScale = 0;
        m_RGB.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_IsMoving = false;
        m_IsBlocking = false;
        m_KnockBackTime = 0f;
        m_AttackRadius = 0.5f;
        m_AttackDist = 1f;
    }

    // Runs every frame. used to control physics movement
    void FixedUpdate()
    {
        if (m_KnockBackTime <= 0)
        {
            CheckInputs();
            AttackController();
            UseShield();
            Movement();
            SendAnimationInformation();
        }
        else
        {
            m_KnockBackTime -= Time.fixedDeltaTime;
        }
    }

    // Get input from controller
    void CheckInputs()
    {
        // Reset movement and sprinting bool
        m_IsMoving = false;
        m_Sprinting = false;

        // Get controller input
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        // If we are moving
        if (hori != 0 || vert != 0)
        {
            m_IsMoving = true;
            m_LastDirection = new Vector2(hori, vert);
        }
        m_DeltaForce = new Vector2(hori, vert).normalized;
        
        // Check if we are holding sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Sprinting = true;
            CreateSprintAnimation();
        }
    }

    // Update Animator component values
    void SendAnimationInformation()
    {
        m_Animator.SetFloat("XSpeed", m_RGB.velocity.x);
        m_Animator.SetFloat("YSpeed", m_RGB.velocity.y);
        m_Animator.SetFloat("XLast", m_LastDirection.x);
        m_Animator.SetFloat("YLast", m_LastDirection.y);
        m_Animator.SetBool("IsMoving", m_IsMoving);
        m_Animator.SetBool("IsAttacking", m_IsAttacking);
        m_Animator.SetBool("IsColliding", m_Collider.IsTouchingLayers(Physics2D.AllLayers));
        m_Animator.SetBool("IsBlocking", m_IsBlocking);
    }

    #region MOVEMENT
    // Add force to rigidbody moving player
    void Movement()
    {
        // Reset velocity
        m_RGB.velocity = Vector2.zero;

        // Check if sprint is being held and select proper speed
        float speed = m_Sprinting ? m_PlayerScript.SprintSpeed : m_PlayerScript.Speed; 

        // Apply force to move player
        m_RGB.AddForce(m_DeltaForce * speed, ForceMode2D.Impulse);
    }

    // Apply knockback to player based on hazard values
    public void ApplyKnockback(float duration, float power, int damage)
    {
        m_KnockBackTime = duration;
        Vector3 vec = -m_LastDirection;
        m_RGB.AddForce(vec * power, ForceMode2D.Impulse);
        if (!m_IsBlocking)
        {
            m_PlayerScript.RemoveHealth(damage);
            StartCoroutine(m_FlashScript.FlashAlpha());
        }
    }

    // Create sprint cloud of dirt
    void CreateSprintAnimation()
    {
        if (Time.time > m_SprintAnimationDelay)
        {
            Instantiate(m_SprintObject, transform.position, Quaternion.identity);
            m_SprintAnimationDelay = Time.time + 0.25f;
        }
    }

    #endregion

    #region COMBAT
    // Attack controller
    void AttackController()
    {
        // Reset attack bool
        m_IsAttacking = false;

        // Check for controller input
        if (m_PlayerScript.FireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > m_PlayerScript.AttackDelay)
            {
                m_PlayerScript.AttackDelay = Time.time + 1 / m_PlayerScript.FireRate;
                Attack();
            }
        }
    }

    // Use Weapon to attack
    void Attack()
    {
        // Set attacking bool
        m_IsAttacking = true;

        float xDir = m_LastDirection.x * m_AttackDist;
        float yDir = m_LastDirection.y * m_AttackDist;

        Vector2 point = new Vector2(transform.position.x + xDir, transform.position.y + yDir);
        Collider2D[] results = new Collider2D[10];
        int hits = Physics2D.OverlapCircleNonAlloc(point, m_AttackRadius, results);

        // if hits is greater than 0 then circle has hit something
        if (hits > 0)
        {
            for (int i = 0; i < hits; i++)
            {
                if (results[i].gameObject.CompareTag("Breakable"))
                {
                    results[i].GetComponent<Breakable>().Break();
                }

                if (results[i].gameObject.CompareTag("Enemy") && !results[i].isTrigger)
                {
                    results[i].GetComponent<LogWalker>().TakeDamage(transform.position, 0.25f, 3);
                }
            }
        }
    }

    // Use shield to avoid attacks
    void UseShield()
    {
        m_IsBlocking = false;
        if (!m_IsMoving)
        {
            if (Input.GetKey(KeyCode.X))
            {
                m_IsBlocking = true;
            }
        }
    }
    #endregion

    

}
