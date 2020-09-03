using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private AudioSource audio;
    private bool IsPushed;
    private float CooldownSound = 2;
    private Rigidbody2D rb;
    public float MassCheck;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CooldownSound -= Time.deltaTime;
        if (IsPushed == true && CooldownSound <= 0)
        {
            playSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass > MassCheck)
            {
                IsPushed = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
             
            }
            else
            {
                IsPushed = false;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            IsPushed = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void playSound()
    {
        audio.Play();
        CooldownSound = 2;
    }
}
