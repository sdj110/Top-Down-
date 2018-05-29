using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // Attributes
    int m_HealthValue;

    // Used to initalize
    void Start()
    {
        m_HealthValue = 4;
    }

    // Runs when collider enters trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().AddHealth(m_HealthValue);
            Destroy(this.gameObject);
        }
    }


}
