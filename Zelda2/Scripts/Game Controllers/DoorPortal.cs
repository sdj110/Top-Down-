using UnityEngine;

public class DoorPortal : MonoBehaviour
{
    // Attributes
    [SerializeField] GameObject m_Sibling;
    [SerializeField] int m_ID;
    public Vector3 PlacementVector;
    Vector3 m_SiblingPlacement;

    // Initialize
    void Start()
    {
        m_SiblingPlacement = m_Sibling.GetComponent<DoorPortal>().PlacementVector;
    }

    // Check for collision with player tag
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            obj.transform.position = m_SiblingPlacement;
        }
    }

}

