using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMiniGame : MonoBehaviour
{
   
    private int _currentCollectibleInstantiated = 0;
    private MiniGameManager miniGameManager;

    [SerializeField] private float minBetweenCollectible = 2;
    [SerializeField] private float maxBetweenCollectible = 4;
    [SerializeField] private int numberCollectibleTotal = 10;
    [SerializeField] private int numberCollectibleToPass = 3;
    [SerializeField] private string[] collectibleTags;
    [SerializeField] private float _collectibleOffsetX;
    [SerializeField] private float _collectibleOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        miniGameManager = MiniGameManager.instance;

        miniGameManager.onChangeState += () =>
        {
            if (miniGameManager.state == State.FIRSTMG)
            {
                _currentCollectibleInstantiated = 0;
                miniGameManager.collectiblesPickedUp = 0;
                StartCoroutine(StartFirstMinigame());
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M) && GameManager.instance.onDebugMode)
        {
            miniGameManager.ChangeState(State.FIRSTMG);
        }
    }

    private IEnumerator StartFirstMinigame()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FirstMinigame());
    }

    private IEnumerator FirstMinigame()
    {
        ObjectSpawner.instance.InstantiateObject(collectibleTags, _collectibleOffsetX, _collectibleOffsetY);
        yield return new WaitForSeconds(Random.Range(minBetweenCollectible, maxBetweenCollectible));
        _currentCollectibleInstantiated += 1;
        if (_currentCollectibleInstantiated < numberCollectibleTotal)
            StartCoroutine(FirstMinigame());
        else
        {
            StartCoroutine(WaitAndEndFirstMiniGame());
        }
    }

    private void CheckIfMiniGamePassed()
    {
        if (miniGameManager.collectiblesPickedUp >= numberCollectibleToPass)
        {
            MiniGameManager.instance.ChangeState(State.NONE);
            if (GameManager.instance.onPhaseChange != null) GameManager.instance.onPhaseChange.Invoke(4);
        }
        else
        {
            GameManager.instance.PlayerDead();
        }
    }

    private IEnumerator WaitAndEndFirstMiniGame()
    {
        yield return new WaitForSeconds(2f);
        CheckIfMiniGamePassed();
    }
}
