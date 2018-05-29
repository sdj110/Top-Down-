using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GenerateItem))]

public class Breakable : MonoBehaviour
{
    // Attributes
    Animator m_Animator;
    BoxCollider2D m_Collider;
    GenerateItem m_GenerateItemScript;

    // Used to get reference to objects components
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_GenerateItemScript = GetComponent<GenerateItem>();
    }

    // Used to initialize attributes
    void Start()
    {
        m_Animator.SetBool("Break", false);
    }

    // Break when hit with weapon
    public void Break()
    {
        m_Animator.SetBool("Break", true);
        m_Collider.enabled = false;
        Destroy(this.gameObject, 2f);
        m_GenerateItemScript.GenerateRandomItem(0.5f);
    }

    
}
