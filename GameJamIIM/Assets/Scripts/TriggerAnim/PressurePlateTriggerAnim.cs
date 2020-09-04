using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTriggerAnim : MonoBehaviour
{
    Animator animPP;
    [SerializeField] bool isLight = false;
    [SerializeField] int counter = 1;
    private void Start()
    {
        animPP = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isLight)
            {
                StartCoroutine(PPAnimation());
            }
            else
            {
                animPP.SetBool("isActivate", false);
                animPP.SetBool("isOn", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isLight)
            {
                isLight = false;
            }
            else
            {
                isLight = true;
            }
        }
    }

    IEnumerator PPAnimation()
    {
        animPP.SetBool("isActivate", true);
        yield return null;
        animPP.SetBool("isOn", true);
    }
}
