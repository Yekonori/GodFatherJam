using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    #region Script Parameters

    #endregion
    #region Fields
    #endregion
    #region Unity Methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("death"))
        {
            Dead();
        }
    }

    void Dead()
    {
        Debug.Log("death");
        gameObject.SetActive(false);
    }
    #endregion
}
