using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPiece : MonoBehaviour
{
    /* GameObject that Link will pick up to add to max health  */

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<Player>().AddHeartContainer();
            Destroy(this.gameObject);
        }
    }
}
