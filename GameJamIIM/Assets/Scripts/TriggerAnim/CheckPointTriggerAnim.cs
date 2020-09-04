using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTriggerAnim : MonoBehaviour
{
    Animator animCheckPoint;
    private void Start()
    {
        animCheckPoint = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(checkPointAnimation()); 
        }
    }

    IEnumerator checkPointAnimation()
    {
        animCheckPoint.SetTrigger("isActivate");
        yield return null;
        animCheckPoint.SetTrigger("isOn");
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    animCheckPoint.SetBool("isCheckPoint", false);
    //}
}
