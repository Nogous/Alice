using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{

    public static InterfaceManager instance;

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    public bool isInPause = false;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);

        MiniGameManager.instance.onChangeState += () =>
        {
            if (MiniGameManager.instance.state == State.DEAD)
            {
                Time.timeScale = 0;
                TimerScore.instance.StopTimer();
                Life.instance.SetLifeUI(false);
                gameOverPanel.SetActive(true);
                gamePanel.SetActive(false);
                pausePanel.SetActive(false);
            }
        };

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

    public void Win()
    {
        Time.timeScale = 0;
        TimerScore.instance.StopTimer();
        Life.instance.SetLifeUI(false);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(true);
    }
}
