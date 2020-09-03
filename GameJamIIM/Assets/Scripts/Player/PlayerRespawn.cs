using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    #region variable
    [SerializeField] Vector3 RespawnPoint;
    [SerializeField] float deathTime = 1f;

    #endregion

    #region System
    
    void Start()
    {
        RespawnPoint = transform.position;
    }

    
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckPoint")
        {
            RespawnPoint = collision.transform.position;
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "death") 
        {
            //Debug.Log("Before Coroutine");
            LauchRevive();
            //Debug.Log("AfterCoroutine");

        }


    }
    IEnumerator Revive()
    {
        gameObject.SetActive(false);
        //Debug.Log("SetActive False");
        transform.position = RespawnPoint;
        //Debug.Log("Teleport ");
        gameObject.SetActive(true);
        //Debug.Log("Revive !");
        yield return new WaitForSeconds(1f);
        //Debug.LogError("Wait");
        
        yield break;
    }

    public void LauchRevive()
    {
        StartCoroutine(Revive());
    }




    #endregion
}
