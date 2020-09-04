using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTriggerAnimation : MonoBehaviour
{
    Animator animBush;
    private void Start()
    {
        animBush = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animBush.SetBool("isBushing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animBush.SetBool("isBushing", false);
    }
}
