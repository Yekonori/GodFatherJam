using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    #region variable
    [SerializeField] Vector3 RespawnPoint;

    #endregion

    #region System
    
    void Start()
    {
        RespawnPoint = transform.position;
    }

    
    void Update()
    {
        Respawn();
    }
    private void Respawn() 
    {
     transform.position = RespawnPoint;
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckPoint")
        {
            RespawnPoint = collision.transform.position;
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    #endregion
}
