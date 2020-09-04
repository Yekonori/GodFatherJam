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
    public bool isBox;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass > MassCheck)
            {
                IsPushed = true;
                //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
            else
            {
                IsPushed = false;
                if (isBox)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsPushed = false;

            if (isBox)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void playSound()
    {
        audio.Play();
        CooldownSound = 2;
    }
}
