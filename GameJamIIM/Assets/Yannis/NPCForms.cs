using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCForms : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private eForms form;

    #endregion

    public eForms GetForm()
    {
        return form;
    }
}
