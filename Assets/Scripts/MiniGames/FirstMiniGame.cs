using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMiniGame : MonoBehaviour
{
    public static FirstMiniGame instance;
    private int _currentCollectibleInstantiated = 0;
    private MiniGameManager miniGameManager;

    [SerializeField] private float minBetweenCollectible = 2;
    [SerializeField] private float maxBetweenCollectible = 4;
    [SerializeField] private string[] collectibleTags;
    [SerializeField] private float _collectibleOffsetX;
    [SerializeField] private float _collectibleOffsetY;

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
        int rand = Random.Range(0, 2);
        float globalTime = 0;
        if(rand == 0)
        {
            ObjectSpawner.instance.InstantiateObject(collectibleTags, _collectibleOffsetX, _collectibleOffsetY);
            _currentCollectibleInstantiated += 1;

            float randTime = Random.Range(0.5f, 1f);
            globalTime += randTime;
            yield return new WaitForSeconds(randTime);
            ObjectSpawner.instance.InstantiateObject(ObjectSpawner.instance.obstacleTags[Random.Range(0, ObjectSpawner.instance.obstacleTags.Length)], _collectibleOffsetX, _collectibleOffsetY);

            randTime = Random.Range(0.5f, 1f);
            globalTime += randTime;
            yield return new WaitForSeconds(randTime);
            ObjectSpawner.instance.InstantiateObject(collectibleTags, _collectibleOffsetX, _collectibleOffsetY);
            _currentCollectibleInstantiated += 1;

        }
        else
        {
            ObjectSpawner.instance.InstantiateObject(ObjectSpawner.instance.obstacleTags[Random.Range(0, ObjectSpawner.instance.obstacleTags.Length)], _collectibleOffsetX, _collectibleOffsetY);

            float randTime = Random.Range(0.5f, 1f);
            globalTime += randTime;
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            ObjectSpawner.instance.InstantiateObject(collectibleTags, _collectibleOffsetX, _collectibleOffsetY);
            _currentCollectibleInstantiated += 1;

            randTime = Random.Range(0.5f, 1f);
            globalTime += randTime;
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            ObjectSpawner.instance.InstantiateObject(ObjectSpawner.instance.obstacleTags[Random.Range(0, ObjectSpawner.instance.obstacleTags.Length)], _collectibleOffsetX, _collectibleOffsetY);
        }
        yield return new WaitForSeconds(Random.Range(minBetweenCollectible, maxBetweenCollectible) - globalTime);
        if (miniGameManager.state == State.FIRSTMG)
            StartCoroutine(FirstMinigame());
    }

    public void StopMiniGame()
    {
        StopAllCoroutines();
        CheckIfMiniGamePassed();
    }

    private void CheckIfMiniGamePassed()
    {
        if (miniGameManager.collectiblesPickedUp >= (_currentCollectibleInstantiated/2) || ignoreThisMiniGame)
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
