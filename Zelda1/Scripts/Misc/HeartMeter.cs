using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMeter : MonoBehaviour
{
    // Attributes
    [SerializeField] GameObject m_HeartContainer;
    List<GameObject> m_HeartList;
    Player m_PlayerScript;
    int m_CurrentHeartIndex;


    // Used to get create reference to component on this gameobject
    void Awake()
    {
        m_PlayerScript = GetComponentInParent<Player>();
    }

    // Used to Initialized attributes
    void Start()
    {
        // Initialize array and index 
        m_HeartList = new List<GameObject>();
        m_CurrentHeartIndex = m_PlayerScript.NumberOfHearts - 1;

        // Create Hearts
        for (int i = 0; i < m_PlayerScript.NumberOfHearts; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + (i), transform.position.y);
            GameObject clone = Instantiate(m_HeartContainer, pos, Quaternion.identity);
            m_HeartList.Add(clone);
        }
    }

    // Runs once a frame
    void Update()
    {
        for (int i = 0; i < m_HeartList.Count; i++)
        {
            if (i < 10)
            {
                m_HeartList[i].transform.position = new Vector3(transform.position.x + (i), transform.position.y);
            }
            else
            {
                m_HeartList[i].transform.position = new Vector3(transform.position.x + (i) - 10, transform.position.y - 1);
            }
        }
    }

    // Increase number of heart containers in HUD
    public void AddHeartContainer()
    {
        // Find last object in heart list for position
        GameObject obj = m_HeartList[0];

        // Create new position vector
        Vector3 pos = new Vector3(obj.transform.position.x + m_HeartList.Count, obj.transform.position.y);
        
        // Create clone heart container in proper position
        GameObject clone = Instantiate(m_HeartContainer, pos, Quaternion.identity);

        // Add new clone to heart list
        m_HeartList.Add(clone);

        // Set counter to highest heart value
        m_CurrentHeartIndex = m_HeartList.Count - 1;
    }

    // Add heart piece back to HUD
    public void AddHeartPiece()
    {
        if (m_HeartList[m_CurrentHeartIndex].GetComponent<HeartHUD>().AddHeartPiece())
        {
            m_CurrentHeartIndex++;
            m_HeartList[m_CurrentHeartIndex].GetComponent<HeartHUD>().AddHeartPiece();
        }
    }

    // Remove health
    public void RemoveHeartPiece()
    {
        if (m_HeartList[m_CurrentHeartIndex].GetComponent<HeartHUD>().RemoveHeartPiece())
        {
            if (m_CurrentHeartIndex > 0)
            {
                m_CurrentHeartIndex--;
                m_HeartList[m_CurrentHeartIndex].GetComponent<HeartHUD>().RemoveHeartPiece();
            }
        }
    }

    // Set player health to zero
    public void SetHealthToZero()
    {
        foreach (GameObject heart in m_HeartList)
        {
            heart.GetComponent<HeartHUD>().EmptyHeartContainer();
        }
    }
}
