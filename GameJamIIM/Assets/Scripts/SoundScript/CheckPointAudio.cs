using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAudio : MonoBehaviour
{
    private AudioSource audio;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audio.Play();
        }
    }

}
