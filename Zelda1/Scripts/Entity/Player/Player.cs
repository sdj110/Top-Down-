using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    // Attributes
    private HeartMeter m_HeartMeterScript;
    private int m_NumberOfHearts;
    private int m_MaxHealth;
    private float m_SprintSpeed;
         
    // Accessors/Mutators
    public float Speed
    {
        get
        {
            return m_Speed;
        }
        set
        {
            m_Speed = value;
        }
    }
    public float AttackDelay
    {
        get
        {
            return m_AttackDelay;
        }
        set
        {
            m_AttackDelay = value;
        }
    }
    public float FireRate
    {
        get
        {
            return m_FireRate;
        }
        set
        {
            m_FireRate = value;
        }
    }
    public int Health
    {
        get
        {
            return m_Health;
        }
        set
        {
            m_Health = value;
        }
    }
    public int NumberOfHearts
    {
        get
        {
            return m_NumberOfHearts;
        }
    }
    public float SprintSpeed
    {
        get
        {
            return m_SprintSpeed;
        }

        set
        {
            m_SprintSpeed = value;
        }
    }


    // Use this for initialization
    void Awake ()
    {
        m_HeartMeterScript = GetComponentInChildren<HeartMeter>();
        m_Name = "Player";
        m_MaxHealth = 12;
        m_Health = m_MaxHealth;
        m_NumberOfHearts = m_MaxHealth / 4;
        m_Speed = 2f;
        m_SprintSpeed = 5f;
        m_AttackDelay = 1f;
        m_FireRate = 3f;
	}


    #region HEALTH CODE
    // Returns true if player health is full
    public bool IsFullHealth()
    {
        if (m_Health == m_MaxHealth)
        {
            return true;
        }
        return false;
    }
	
    // increase the players health by value and Updates health meter
    public void AddHealth(int value)
    {
        if (!IsFullHealth())
        {
            if (m_Health + value > m_MaxHealth)
            {
                int refill = m_MaxHealth - m_Health;
                m_Health = m_MaxHealth;
                for (int i = 0; i < refill; i++)
                {
                    m_HeartMeterScript.AddHeartPiece();
                }
            }
            else
            {
                m_Health += value;
                for (int i = 0; i < value; i++)
                {
                    m_HeartMeterScript.AddHeartPiece();
                }
            }
        }
    }

    // Remove Health from player and update health meter
    public void RemoveHealth(int value)
    {
        if (m_Health - value <= 0)
        {
            // TODO: DIE
            print("TODO: CREATE DEAD");
            m_Health = 0;
            m_HeartMeterScript.SetHealthToZero();
        }
        else
        {
            m_Health -= value;
            for (int i = 0; i < value; i++)
            {
                m_HeartMeterScript.RemoveHeartPiece();
            }
        }
    }

    // Increase health by 4 and thus adding extra heart container
    public void AddHeartContainer()
    {
        // Max hearts is always 20
        int MaxNumHearts = 20;

        // Fill health meter
        int diff = m_MaxHealth - m_Health;
        AddHealth(diff);

        // Add heart piece if we are not at max total health
        if (m_NumberOfHearts < MaxNumHearts)
        {
            // Reset health value
            m_MaxHealth += 4;
            m_Health = m_MaxHealth;
            m_NumberOfHearts++;
            m_HeartMeterScript.AddHeartContainer();
        }
    }
    //
    #endregion













    void Test()
    {
        RemoveHealth(5);
    }

    void Test2()
    {
        AddHealth(4);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Test();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Test2();
        }
    }
}
