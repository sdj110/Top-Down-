using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class FollowCamera : MonoBehaviour
{

    // Attributes
    [SerializeField] Boundary m_Boundary;
    Transform m_Target;
    

    // Used to initialize
    void Start()
    {
        GameObject tmp = GameObject.FindWithTag("Player");
        if (tmp != null)
        {
            m_Target = tmp.transform;
        }
        if (m_Target == null)
        {
            Debug.LogError("Cannot find Transform with tag 'Player' in FollowCamera");
        }
    }

    // LateUpdate runs once a frame after other Updates
    void LateUpdate()
    {
        Vector3 pos = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
        transform.position = pos;
    }


}
