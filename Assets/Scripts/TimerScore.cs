using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScore : MonoBehaviour
{
    #region Script Parameters

    public static TimerScore instance;

    public float secretTimer = 50f;

    #endregion

    #region Fields

    [SerializeField] private float _currentTimer;
    private bool _isRunning;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        instance = this;
        StartTimer();
    }

    private void Update()
    {
        if (_isRunning)
        {
            _currentTimer += Time.deltaTime;
        }
    }

    #endregion

    public void StartTimer()
    {
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public float ReturnTimer()
    {
        _isRunning = false;

        return _currentTimer;
    }
}
