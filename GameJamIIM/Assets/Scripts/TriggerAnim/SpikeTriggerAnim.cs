using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTriggerAnim : MonoBehaviour
{
    Animator animSpike;
    private void Start()
    {
        animSpike = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animSpike.SetBool("isSpike", true);
        }
    }
}
