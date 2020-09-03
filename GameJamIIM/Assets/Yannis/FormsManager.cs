using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormsManager : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private GameObject basicForm;
    [SerializeField] private GameObject balloonForm;
    [SerializeField] private GameObject nainForm;

    #endregion

    #region Singleton

    private static FormsManager _instance = null;

    public static FormsManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public GameObject GetForm(eForms forms)
    {
        GameObject playerForm = null;

        switch (forms)
        {
            case eForms.BASE:
                playerForm = basicForm;
                break;
            case eForms.BALLOON:
                break;
            case eForms.NAIN:
                playerForm = nainForm;
                break;
            default:
                break;
        }

        return playerForm;
    }

    public CapsuleCollider2D GetCapsuleForm(eForms forms)
    {
        CapsuleCollider2D playerMovement = null;

        switch (forms)
        {
            case eForms.BASE:
                playerMovement = basicForm.GetComponent<CapsuleCollider2D>();
                break;
            case eForms.BALLOON:
                playerMovement = balloonForm.GetComponent<CapsuleCollider2D>();
                break;
            case eForms.NAIN:
                playerMovement = nainForm.GetComponent<CapsuleCollider2D>();
                break;
            default:
                break;
        }

        return playerMovement;
    }

    public Rigidbody2D GetRBForm(eForms forms)
    {
        Rigidbody2D playerMovement = null;

        switch (forms)
        {
            case eForms.BASE:
                playerMovement = basicForm.GetComponent<Rigidbody2D>();
                break;
            case eForms.BALLOON:
                playerMovement = balloonForm.GetComponent<Rigidbody2D>();
                break;
            case eForms.NAIN:
                playerMovement = nainForm.GetComponent<Rigidbody2D>();
                break;
            default:
                break;
        }

        return playerMovement;
    }

    public PlayerMovement GetMovementPlayer(eForms forms)
    {
        PlayerMovement playerMovement = null;

        switch (forms)
        {
            case eForms.BASE:
                playerMovement = basicForm.GetComponent<PlayerMovement>();
                break;
            case eForms.BALLOON:
                playerMovement = balloonForm.GetComponent<PlayerMovement>();
                break;
            case eForms.NAIN:
                playerMovement = nainForm.GetComponent<PlayerMovement>();
                break;
            default:
                break;
        }

        return playerMovement;
    }
}
