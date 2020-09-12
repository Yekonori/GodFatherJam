using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    #region Script Parameters

    [Header("Points")]
    [SerializeField] private Transform points;

    [Header("Platform Values")]
    [SerializeField] private float platformSpeed = 2f;
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private bool loopPath;
    [SerializeField] private bool uniquePath;
    [SerializeField] private bool isAutomatique;
    [SerializeField] private AudioSource audio;

    #endregion

    #region Fields

    private List<Vector3> _pointsList;

    private Vector3 _targetPoint;
    private int _targetIndex;

    private float _tolerance;
    private float _currentDelayTime;

    private bool _isReversePath;

    #endregion

    #region Unity Methods

    private void Start()
    {
        GetAllPoints();

        _tolerance = platformSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (isAutomatique)
        {
            if (transform.position != _targetPoint)
            {
                MovePlatform();
            }
            else
            {
                if (_currentDelayTime < delayTime)
                {
                    UpdatePlatformTimer();
                }
                else
                {
                    UpdateTargetIndex();
                }
            }
        }
    }

    /**
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
    **/

    #endregion

    #region Points

    private void GetAllPoints()
    {
        _pointsList = new List<Vector3>();

        foreach (Transform child in points)
        {
            _pointsList.Add(child.position);
        }

        if (_pointsList.Count > 0)
        {
            SetInitialTarget();
        }
    }

    private void SetInitialTarget()
    {
        _targetIndex = 0;
        SetTarget();
    }

    #endregion

    #region Platform

    private void MovePlatform()
    {
        //if (!audio.isPlaying)
        //{
        //    audio.Play();
        //}

        Vector3 heading = _targetPoint - transform.position;
        transform.Translate((heading / heading.magnitude) * platformSpeed * Time.fixedDeltaTime);

        if (heading.magnitude <= _tolerance)
        {
            transform.position = _targetPoint;
        }
    }

    private void UpdatePlatformTimer()
    {
        _currentDelayTime += Time.fixedDeltaTime;
    }

    #endregion

    #region Target

    private void UpdateTargetIndex()
    {
        if (uniquePath)
        {
            UpdateUniquePath();
        }

        if (loopPath && !uniquePath)
        {
            UpdateLoopPath();
        }
        else if (!loopPath && !uniquePath)
        {
            UpdateLinearPath();
        }

        SetTarget();
    }

    private void UpdateLinearPath()
    {
        if (IsLastIndex())
        {
            SetReversePath(true);
        }

        if (_isReversePath)
        {
            PreviousIndex();
        }
        else
        {
            NextIndex();
        }

        if (_isReversePath && _targetIndex == 0)
        {
            SetReversePath(false);
        }
    }

    private void UpdateLoopPath()
    {
        if (IsLastIndex())
        {
            _targetIndex = 0;
        }
        else
        {
            NextIndex();
        }
    }

    private void UpdateUniquePath()
    {
        if (IsLastIndex())
        {
            return;
        }
        else
        {
            NextIndex();
        }
    }

    private void NextIndex()
    {
        _targetIndex++;
    }

    private void PreviousIndex()
    {
        _targetIndex--;

    }

    private void SetTarget()
    {
        _currentDelayTime = 0f;

        _targetPoint = _pointsList[_targetIndex];
    }

    private bool IsLastIndex()
    {
        if (_targetIndex == _pointsList.Count - 1)
        {
            return true;
        }

        return false;
    }

    private void SetReversePath(bool isReverse)
    {
        _isReversePath = isReverse;
    }

    #endregion

    public void ActiveAutomatic()
    {
        isAutomatique = !isAutomatique;
    }
}
