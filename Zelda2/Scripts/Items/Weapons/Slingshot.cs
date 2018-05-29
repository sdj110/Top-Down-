using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : Weapon
{
    // Attributes
    [SerializeField] GameObject m_Pebble;

    public override int GetDamage()
    {
        return m_Damage;
    }
    public override float GetSpeed()
    {
        return m_Speed;
    }
    public override float GetFireRate()
    {
        return m_FireRate;
    }

    // Used to initialize
    void Start()
    {
        m_Speed = 10f;
        m_FireRate = 1f;
        m_Damage = 1;
    }

    // Override from parent class, used to run specific projectile logic
    public override void Shoot(Transform t, Vector2 dir)
    {
        GameObject pebble = Instantiate(m_Pebble, t.position, Quaternion.identity);
        Pebble pebbleScript = pebble.GetComponent<Pebble>();
        pebbleScript.direction = dir;
    }


}
