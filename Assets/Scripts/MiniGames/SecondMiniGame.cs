using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMiniGame : MonoBehaviour
{

    private MiniGameManager miniGameManager;
    public static SecondMiniGame instance;

    [SerializeField] private SecondMiniGameInfo[] _gameStructure;
    public bool ignoreThisMiniGame = false;

    private void Awake()
    {
        instance = this;
    }

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

        if (ignoreThisMiniGame)
        {
            for(int i = 0; i < _gameStructure.Length; i++)
            {
                foreach (GameObject structure in _gameStructure[i].obstacles)
                {
                    structure.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSecondMiniGame()
    {
        for (int i = 0; i < _gameStructure.Length; i++)
        {
            foreach (GameObject structure in _gameStructure[i].obstacles)
            {
                structure.SetActive(false);
            }
        }
    }

}
