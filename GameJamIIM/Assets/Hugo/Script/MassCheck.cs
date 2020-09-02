using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassCheck : MonoBehaviour
{
    public GameObject Mecanic;
    public Rigidbody2D rb;
    public float MassChek;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SpringTrigger")
        {
            Destroy(Mecanic);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.GetComponent<Rigidbody2D>().mass);
           if( gameObject.GetComponent<Rigidbody2D>().mass > MassChek) 
            {

                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
