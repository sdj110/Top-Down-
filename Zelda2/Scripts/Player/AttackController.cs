using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // Attributes
    [SerializeField] GameObject[] m_Weapons;
    Rigidbody2D m_RGB;
    KeyboardController m_ControllerScript;
    Vector2 m_LastDirection;
    int m_CurrentWeapon;
    float m_TimeToFire;

    // Get components attatched to object
    void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();
        m_ControllerScript = GetComponent<KeyboardController>();   
    }

    // Used to initalize
    void Start()
    {
        m_CurrentWeapon = 0;
        m_LastDirection = Vector2.down;
    }

    // Runs once a frame
    void Update()
    {
        SetLastDirection();
        CheckForAttackInput();
    }

    // Find last direction
    void SetLastDirection()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        if (m_RGB.velocity.x != 0 || m_RGB.velocity.y != 0)
        {
            m_LastDirection = new Vector2(m_RGB.velocity.x, m_RGB.velocity.y).normalized;
        }
    }

    // Pool input for attacks
    void CheckForAttackInput()
    {
        Weapon weapon = m_Weapons[m_CurrentWeapon].GetComponent<Weapon>();
        // fire mechanism 
        if (weapon.GetFireRate() == 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                weapon.Shoot(transform, m_LastDirection);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F) && Time.time > m_TimeToFire)
            {
                m_TimeToFire = Time.time + 1 / weapon.GetFireRate();
                weapon.Shoot(transform, m_LastDirection);
            }
        }

    }
}
