using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormsManager : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private GameObject basicForm;
    [SerializeField] private GameObject bigForm;
    [SerializeField] private GameObject tallForm;
    [SerializeField] private GameObject balloonForm;
    [SerializeField] private GameObject nainForm;

    [SerializeField] private Animator basicAnimator;
    [SerializeField] private Animator bigAnimator;
    [SerializeField] private Animator tallAnimator;
    [SerializeField] private Animator balloonAnimator;
    [SerializeField] private Animator nainAnimator;

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
            case eForms.BIG:
                playerForm = bigForm;
                break;
            case eForms.TALL:
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

    public Animator GetAnimatorForm(eForms forms)
    {
        Animator playerForm = null;

        switch (forms)
        {
            case eForms.BASE:
                playerForm = basicAnimator;
                break;
            case eForms.BIG:
                playerForm = bigAnimator;
                break;
            case eForms.TALL:
                break;
            case eForms.BALLOON:
                break;
            case eForms.NAIN:
                playerForm = nainAnimator;
                break;
            default:
                break;
        }

        return playerForm;
    }

    public PlayerMovement GetMovementPlayer(eForms forms)
    {
        PlayerMovement playerMovement = null;

        switch (forms)
        {
            case eForms.BASE:
                playerMovement = basicForm.GetComponent<PlayerMovement>();
                break;
            case eForms.BIG:
                playerMovement = bigForm.GetComponent<PlayerMovement>();
                break;
            case eForms.TALL:
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
