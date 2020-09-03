using System.Collections.Generic;
using UnityEngine;

public class MassCheck : MonoBehaviour
{
    public List<VerticalPlatform> Mecanic;
    public Rigidbody2D rb;
    private AudioSource audio;
    public float MassChek;


    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            
            foreach(VerticalPlatform plat in Mecanic)
            {
                Destroy(plat);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }

            if (collision.gameObject.GetComponent<Rigidbody2D>().mass > MassChek)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                foreach (VerticalPlatform plat in Mecanic)
                {
                    plat.ActiveAutomatic();
                }
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        
    }
}
