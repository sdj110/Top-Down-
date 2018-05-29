using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    // Attributes
    int m_Damage;
    float m_Power;
    float m_Time;


    void Start()
    {
        m_Damage = 1;
        m_Power = 3;
        m_Time = 0.25f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<PlayerController>().ApplyKnockback(m_Time, m_Power, m_Damage);
        }
    }

}
