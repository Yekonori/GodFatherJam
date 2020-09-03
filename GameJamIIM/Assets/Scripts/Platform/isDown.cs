using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            transform.parent.GetComponent<Collider2D>().enabled = false;
        }
    }
}
