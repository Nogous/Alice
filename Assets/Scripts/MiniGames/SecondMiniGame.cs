using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMiniGame : MonoBehaviour
{

    private MiniGameManager miniGameManager;

    [SerializeField] private GameObject _infoPanel;

    // Start is called before the first frame update
    void Start()
    {
        miniGameManager = MiniGameManager.instance;

        miniGameManager.onChangeState += () =>
        {
            if (miniGameManager.state == State.SECONDMG)
            {
                //StartCoroutine(StartSecondMinigame());
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartSecondMinigame()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartSecondMiniGame());
    }

    private IEnumerator StartSecondMiniGame()
    {
        _infoPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        _infoPanel.SetActive(false);
        MiniGameManager.instance.ChangeState(State.NONE);
        if (GameManager.instance.onPhaseChange != null) GameManager.instance.onPhaseChange.Invoke(6);
    }

}
