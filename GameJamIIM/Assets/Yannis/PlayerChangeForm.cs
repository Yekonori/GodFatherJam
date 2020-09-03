using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeForm : MonoBehaviour
{
    #region Script Parameters

    //[SerializeField] private GameObject characterHolder;
    [SerializeField] private PlayerMovement pM;
    [SerializeField] private CapsuleCollider2D cc2D;
    [SerializeField] private Rigidbody2D rb2D;

    /**
     * Normal & Gros : 2 mass
     * Nain & Balloon : 1 mass
     */
    

    [SerializeField] private GameObject basicForm;
    [SerializeField] private GameObject smallForm;
    [SerializeField] private GameObject balloonForm;

    #endregion

    #region Fields

    [SerializeField] private bool _isTrigger = false;
    [SerializeField] private eForms _triggerForm = eForms.BASE;

    #endregion

    #region Unity Methods

    private void Start()
    {
        basicForm.SetActive(true);
        smallForm.SetActive(false);
        balloonForm.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_isTrigger)
            {
                CopycatForm();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCForms otherNPC = collision.GetComponent<NPCForms>();

        if (otherNPC != null)
        {
            _isTrigger = true;
            _triggerForm = otherNPC.GetForm();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCForms otherNPC = collision.GetComponent<NPCForms>();

        if (otherNPC != null)
        {
            _isTrigger = false;
        }
    }

    #endregion

    private void CopycatForm()
    {
        //PlayerChangeForm newForm = FormsManager.Instance.GetForm(_triggerForm).GetComponent<PlayerChangeForm>();

        //GameObject newCharacter = newForm.characterHolder.transform.GetChild(0).gameObject;
        //pM.Animator = newForm.pM.Animator;

        //Destroy(characterHolder.transform.GetChild(0).gameObject);
        //Instantiate(newCharacter, characterHolder.transform);

        switch(_triggerForm)
        {
            case eForms.BASE:
                basicForm.SetActive(true);
                smallForm.SetActive(false);
                balloonForm.SetActive(false);
                //pM.Animator.runtimeAnimatorController = basicForm.GetComponent<PlayerMovement>().Animator.runtimeAnimatorController;
                break;
            case eForms.NAIN:
                basicForm.SetActive(false);
                smallForm.SetActive(true);
                balloonForm.SetActive(false);
                //pM.Animator.runtimeAnimatorController = smallForm.GetComponent<PlayerMovement>().Animator.runtimeAnimatorController;
                break;
            case eForms.BALLOON:
                basicForm.SetActive(false);
                smallForm.SetActive(false);
                balloonForm.SetActive(true);
                break;
        }

        CopyCatValue();
        CopyCatCapsule();
        CopycatRB();
    }

    private void CopyCatValue()
    {
        PlayerMovement pMForm = FormsManager.Instance.GetMovementPlayer(_triggerForm);

        if (pMForm != null)
        {
            pM.CopyCatPlayerMovement(pMForm);
        }
    }

    private void CopyCatCapsule()
    {
        CapsuleCollider2D pMForm = FormsManager.Instance.GetCapsuleForm(_triggerForm);

        if (pMForm != null)
        {
            cc2D.offset = pMForm.offset;
            cc2D.size = pMForm.size;
        }
    }

    private void CopycatRB()
    {
        switch (_triggerForm)
        {
            case eForms.BASE:
                rb2D.mass = 2;
                break;
            case eForms.NAIN:
                rb2D.mass = 1;
                break;
            case eForms.BALLOON:
                rb2D.mass = 1;
                break;
        }
    }
}
