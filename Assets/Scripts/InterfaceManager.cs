using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;

    private bool isInPause = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isInPause)
                PauseTheGame();
            else
            {
                Resume();
            }
        }
    }

    public void PauseTheGame()
    {
        isInPause = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void Resume()
    {
        isInPause = false;
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
