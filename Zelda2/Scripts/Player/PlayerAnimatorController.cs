using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimatorController : MonoBehaviour
{
    // Attributes
    // Components
    Animator m_Animator;
    Rigidbody2D m_RGB;

    // Animator Parameters
    Vector2 m_LastDirection;
    bool m_Moving;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_RGB = GetComponentInParent<Rigidbody2D>();
    }

    // Runs once a frame. Used to update the Animator attatched to object
    void Update()
    {
        CheckForMotion();
        UpdateAnimator();
    }

    // Check Rigidbody to determine if we are moving
    void CheckForMotion()
    {
        m_Moving = false;
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        
        if (m_RGB.velocity.x != 0 || m_RGB.velocity.y != 0)
        {
            m_Moving = true;
            m_LastDirection = new Vector2(m_RGB.velocity.x, m_RGB.velocity.y);
        }
    }

    // Update Amimator parameters
    void UpdateAnimator()
    {
        m_Animator.SetBool("IsMoving", m_Moving);
        m_Animator.SetFloat("XSpeed", m_RGB.velocity.x);
        m_Animator.SetFloat("YSpeed", m_RGB.velocity.y);
        m_Animator.SetFloat("XLast", m_LastDirection.x);
        m_Animator.SetFloat("YLast", m_LastDirection.y);
    }

}
