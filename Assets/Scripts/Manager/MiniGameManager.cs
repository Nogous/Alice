using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { NONE, FIRSTMG, SECONDMG, THIRDMG, FOURTHMG}
public class MiniGameManager : MonoBehaviour
{

    public static MiniGameManager instance;

    public State state;
    public System.Action onChangeState;

    [Header("Mini-game 1")]
    [SerializeField] private float _minBetweenCollectible;
    [SerializeField] private float _maxBetweenCollectible;
    [SerializeField] private int _numberCollectibleTotal = 10;
    [SerializeField] private int _numberCollectibleToPass = 3;
    private int _currentCollectibleInstantiated = 0;
    public int collectiblesPickedUp = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        onChangeState += () =>
        {
            if (state == State.FIRSTMG)
            {
                _currentCollectibleInstantiated = 0;
                collectiblesPickedUp = 0;
                StartCoroutine(FirstMinigame());
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.M) && GameManager.instance.onDebugMode)
        {
            state = State.FIRSTMG;
            if (onChangeState != null) onChangeState.Invoke();
        }
    }

    private IEnumerator FirstMinigame()
    {
        ObjectSpawner.instance.InstantiateObject("Collectible");
        yield return new WaitForSeconds(Random.Range(_minBetweenCollectible, _maxBetweenCollectible));
        _currentCollectibleInstantiated += 1;
        if(_currentCollectibleInstantiated < _numberCollectibleTotal)
            StartCoroutine(FirstMinigame());
        else
        {
            CheckIfMiniGamePassed();
        }
    }

    private void CheckIfMiniGamePassed()
    {
        if(collectiblesPickedUp >= _numberCollectibleToPass)
        {
            //go to second minigame
            print("you passed");
        }
        else
        {
            //gameover
            print("you lost");
        }
    }

}
