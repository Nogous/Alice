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
                StartCoroutine(FirstMinigame());
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M) && GameManager.instance.onDebugMode)
        {
            miniGameManager.state = State.FIRSTMG;
            if (miniGameManager.onChangeState != null) miniGameManager.onChangeState.Invoke();
        }
    }

    private IEnumerator FirstMinigame()
    {
        ObjectSpawner.instance.InstantiateObject("Collectible");
        yield return new WaitForSeconds(Random.Range(minBetweenCollectible, maxBetweenCollectible));
        _currentCollectibleInstantiated += 1;
        if (_currentCollectibleInstantiated < numberCollectibleTotal)
            StartCoroutine(FirstMinigame());
        else
        {
            CheckIfMiniGamePassed();
        }
    }

    private void CheckIfMiniGamePassed()
    {
        if (miniGameManager.collectiblesPickedUp >= numberCollectibleToPass)
        {
            //go to second minigame
            print("you passed");
            miniGameManager.state = State.NONE;
            if (miniGameManager.onChangeState != null) miniGameManager.onChangeState.Invoke();
        }
        else
        {
            //gameover
            print("you lost");
        }
    }
}
