using System.Collections.Generic;
using UnityEngine;

public class MassMultipleCheck : MonoBehaviour
{
    public PoundPlatform Mecanic;
    public Rigidbody2D rb;
    public float MassChek;

    [SerializeField] private float _massTotal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rbCollision = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rbCollision != null)
        {
            AddMass(rbCollision.mass);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D rbCollision = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rbCollision != null)
        {
            RemoveMass(rbCollision.mass);
        }
    }

    private void AddMass(float mass)
    {
        _massTotal += mass;
        CheckMass();
    }

    private void RemoveMass(float mass)
    {
        _massTotal -= mass;
        CheckMass();
    }

    private void CheckMass()
    {
        if (_massTotal > MassChek)
        {
            Mecanic.GoDown();
        }
        else
        {
            Mecanic.GoUp();
        }
    }
}
