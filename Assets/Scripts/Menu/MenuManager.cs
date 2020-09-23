﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject basePanel;
    [SerializeField] private GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        GoToBasePanel();
    }

    public void Play()
    {
        SceneManager.LoadScene("MatPathObstacle");
    }

    public void GoToBasePanel()
    {
        basePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void Options()
    {
        basePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
