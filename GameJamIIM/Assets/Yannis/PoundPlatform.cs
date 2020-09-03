using System.Collections.Generic;
using UnityEngine;

public class PoundPlatform : MonoBehaviour
{
    #region Script Parameters

    [Header("Points")]
    [SerializeField] private Transform points;

    [Header("Platform Values")]
    [SerializeField] private float platformSpeed = 2f;

    #endregion

    #region Fields

    private List<Vector3> _pointsList;

    public Vector3 _targetPoint;
    public int _targetIndex;

    private float _tolerance;
    private float _currentDelayTime;

    #endregion

    #region Unity Methods

    private void Start()
    {
        GetAllPoints();

        _tolerance = platformSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (transform.position != _targetPoint)
        {
            MovePlatform();
        }
    }

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
        _targetPoint = _pointsList[_targetIndex];
    }

    #endregion

    #region Platform

    private void MovePlatform()
    {
        Vector3 heading = _targetPoint - transform.position;
        transform.Translate((heading / heading.magnitude) * platformSpeed * Time.fixedDeltaTime);

        if (heading.magnitude <= _tolerance)
        {
            transform.position = _targetPoint;
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
        _targetPoint = _pointsList[_targetIndex];
    }

    #endregion

    public void GoDown()
    {
        NextIndex();
        SetTarget();
    }

    public void GoUp()
    {
        PreviousIndex();
        SetTarget();
    }
}
