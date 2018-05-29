using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItem : MonoBehaviour
{
    // Attributes
    [SerializeField] GameObject[] m_Items;
    [SerializeField] int m_Chance;

    // Generate one of the items randomly
    public void GenerateRandomItem(float delay)
    {
        int chance = Random.Range(0, m_Chance);
        if (chance == 0)
        {
            Invoke("CreateFromList", delay);
        }
    }

    // Instantate the randomly created item
    void CreateFromList()
    {
        Instantiate(m_Items[Random.Range(0, m_Items.Length - 1)], transform.position, Quaternion.identity);
    }
}
