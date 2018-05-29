using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHUD : MonoBehaviour
{
    /* HUD Representation of a single life hud heart (4 health - 1 for each piece) */

    // Attributes
    [SerializeField] Sprite[] m_Sprites;
    int m_index;
    SpriteRenderer m_SpriteRenderer;

    // Get Access to components
    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Initialize 
    void Start()
    {
        m_index = 0;
        m_SpriteRenderer.sprite = m_Sprites[m_index];
    }

    // Refill heart piece container
    public bool AddHeartPiece()
    {
        if (m_index > 0)
        {
            m_index--;
            m_SpriteRenderer.sprite = m_Sprites[m_index];
            return false;
        }
        return true;
    }

    // remove piece of heart in HUD
    public bool RemoveHeartPiece()
    {
        if (m_index < m_Sprites.Length - 1)
        {
            m_index++;
            m_SpriteRenderer.sprite = m_Sprites[m_index];
            return false;
        }
        return true;
    }

    // Sets Heart sprite to empty
    public void EmptyHeartContainer()
    {
        m_SpriteRenderer.sprite = m_Sprites[m_Sprites.Length - 1];
    }

}
