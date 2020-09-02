﻿using System.Collections;
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

    public PlayerMovement GetMovementPlayer(eForms forms)
    {
        PlayerMovement playerMovement = null;

        switch (forms)
        {
            case eForms.BASE:
                break;
            case eForms.BOX:
                break;
            case eForms.CAPSULE:
                break;
            case eForms.BIG:
                playerMovement = bigForm.GetComponent<PlayerMovement>();
                break;
            case eForms.TALL:
                break;
            case eForms.BALLOON:
                break;
            case eForms.NAIN:
                break;
            default:
                break;
        }

        return playerMovement;
    }
}
