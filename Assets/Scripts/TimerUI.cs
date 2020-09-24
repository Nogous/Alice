using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private Text timerText;

    #endregion

    #region Unity Methods

    private void Update()
    {
        timerText.text = Mathf.RoundToInt(TimerScore.instance.GetTimer()).ToString();
    }

    #endregion
}
