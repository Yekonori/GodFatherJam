using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeForm : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private PlayerMovement pM;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject legL;
    [SerializeField] private GameObject legR;

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

        {
            SpriteRenderer headSprite = head.GetComponent<SpriteRenderer>();
            headSprite.sprite = newForm.head.GetComponent<SpriteRenderer>().sprite;

            head.transform.position = new Vector3(newForm.head.transform.position.x, newForm.head.transform.position.y);
        }

        {
            SpriteRenderer bodySprite = body.GetComponent<SpriteRenderer>();
            bodySprite.sprite = newForm.body.GetComponent<SpriteRenderer>().sprite;

            body.transform.position = new Vector3(newForm.body.transform.position.x, newForm.body.transform.position.y);
        }

        //head.sprite = newForm.head.sprite;
        //body.sprite = newForm.body.sprite;
        //legL.sprite = newForm.legL.sprite;
        //legR.sprite = newForm.legR.sprite;

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
