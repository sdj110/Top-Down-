using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Attributes
    protected float m_Speed;
    protected float m_FireRate;
    protected int m_Damage;

    public abstract void Shoot(Transform t, Vector2 dir);
    public abstract float GetSpeed();
    public abstract float GetFireRate();
    public abstract int GetDamage();



}
