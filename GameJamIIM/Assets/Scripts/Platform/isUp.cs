using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isUp : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && player.GetComponent<PlayerMovement>().onGround){
            transform.parent.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<Collider2D>().enabled = true;
    }
}
