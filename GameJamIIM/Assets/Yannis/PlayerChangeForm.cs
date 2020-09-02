﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeForm : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private GameObject characterHolder;
    [SerializeField] private PlayerMovement pM;

    #endregion

    #region Fields

    [SerializeField] private bool _isTrigger = false;
    [SerializeField] private eForms _triggerForm = eForms.BASE;

    #endregion

    #region Unity Methods

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (_isTrigger)
            {
                CopycatForm();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter");

        NPCForms otherNPC = collision.GetComponent<NPCForms>();

        if (otherNPC != null)
        {
            _isTrigger = true;
            _triggerForm = otherNPC.GetForm();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit");

        NPCForms otherNPC = collision.GetComponent<NPCForms>();

        if (otherNPC != null)
        {
            _isTrigger = false;
        }
    }

    #endregion

    private void CopycatForm()
    {
        PlayerChangeForm newForm = FormsManager.Instance.GetForm(_triggerForm).GetComponent<PlayerChangeForm>();

        GameObject newCharacter = newForm.characterHolder.transform.GetChild(0).gameObject;
        pM.Animator = newForm.pM.Animator;

        Destroy(characterHolder.transform.GetChild(0).gameObject);
        Instantiate(newCharacter, characterHolder.transform);

        CopyCatValue();
    }

    private void CopyCatValue()
    {
        PlayerMovement pMForm = FormsManager.Instance.GetMovementPlayer(_triggerForm);

        if (pMForm != null)
        {
            pM.CopyCatPlayerMovement(pMForm);
        }
    }
}
