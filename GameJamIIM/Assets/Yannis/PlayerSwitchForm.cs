using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchForm : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private List<GameObject> playerForms;
    [SerializeField] private float timerSwitchForm = 3f;

    #endregion

    #region Fields

    private int _formIndex = 0;
    private float _countDownForm = 0f;
    private bool _canChangeForm = true;

    #endregion

    #region Unity Methods

    private void Update()
    {
        if (!_canChangeForm)
        {
            CountDownChangeForm();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_canChangeForm)
            {
                ChangeForm();
            }
        }
    }

    #endregion

    private void ChangeForm()
    {
        int lastIndex = _formIndex;

        if (_formIndex == playerForms.Count - 1)
        {
            _formIndex = 0;
        }
        else
        {
            _formIndex++;
        }

        transform.GetChild(lastIndex).gameObject.SetActive(false);
        transform.GetChild(_formIndex).gameObject.SetActive(true);

        CanChangeForm(false);
    }

    private void CountDownChangeForm()
    {
        _countDownForm += Time.fixedDeltaTime;

        if (_countDownForm >= timerSwitchForm)
        {
            _canChangeForm = true;
            _countDownForm = 0f;
        }
    }

    private void CanChangeForm(bool can)
    {
        _canChangeForm = can;
    }
}
