﻿using System.Collections.Generic;
using UnityEngine;

public class MassCheck : MonoBehaviour
{
    public List<VerticalPlatform> Mecanic;
    public Rigidbody2D rb;
    public float MassChek;

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
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass > MassChek)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if (collision.gameObject.tag == "Trigger")
        {
            foreach (VerticalPlatform plat in Mecanic)
            {
                plat.ActiveAutomatic();
            }
        }
    }
}
