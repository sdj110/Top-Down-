using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    // Attributes
    [SerializeField] float m_Delay;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, m_Delay);
    }

}
