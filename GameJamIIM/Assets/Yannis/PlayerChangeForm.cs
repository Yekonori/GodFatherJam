using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeForm : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private GameObject squareBody;
    [SerializeField] private GameObject capsuleBody;

    #endregion

    #region Fields

    private bool _isTrigger = false;
    private eForms _triggerForm = eForms.BASE;

    #endregion

    #region Unity Methods

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_isTrigger)
            {
                TriggerForm();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        NPCForms otherNPC = other.GetComponent<NPCForms>();

        if (otherNPC != null)
        {
            _isTrigger = true;
            _triggerForm = otherNPC.GetForm();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");

        NPCForms otherNPC = other.GetComponent<NPCForms>();

        if (otherNPC != null)
        {
            _isTrigger = false;
        }
    }

    #endregion

    private void TriggerForm()
    {
        GameObject bodyForm = transform.GetChild(0).gameObject;

        switch(_triggerForm)
        {
            case eForms.BASE:
                break;
            case eForms.BOX:
                Destroy(bodyForm);
                Instantiate(squareBody, transform);
                break;
            case eForms.CAPSULE:
                Destroy(bodyForm);
                Instantiate(capsuleBody, transform);
                break;
        }
    }
}
