using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatMinigame : MonoBehaviour
{
    public static HatMinigame instance;

    [SerializeField] private float _minBetweenHat;
    [SerializeField] private float _maxBetweenHat;
    [SerializeField] private int _numberCollectibleTotal = 10;
    [SerializeField] private int _numberCollectibleToPass = 3;
    private int _currentCollectibleInstantiated = 0;
    public int collectiblesPickedUp = 0;

    private float timerSpawn;
    public BigHat bigHat;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    public void StartMiniGameHat()
    {
        if (MiniGameManager.instance.state == State.FOURTHMG)
        {
            _currentCollectibleInstantiated = 0;
            collectiblesPickedUp = 0;

            timerSpawn = Random.Range(_minBetweenHat, _maxBetweenHat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MiniGameManager.instance!=null)
        if (MiniGameManager.instance.state != State.FOURTHMG) return;

        timerSpawn -= Time.deltaTime;

        if (timerSpawn<= 0f)
        {
            if (_currentCollectibleInstantiated > _numberCollectibleTotal)
            {
                CheckIfMiniGamePassed();
                return;
            }

            timerSpawn = Random.Range(_minBetweenHat, _maxBetweenHat);
            ObjectSpawner.instance.InstantiateObject("Hat", ObjectSpawner.instance.xBigOffset, ObjectSpawner.instance.yBigOffset);
            _currentCollectibleInstantiated += 1;
        }
    }

    public void StartMiniGame()
    {
        bigHat.gameObject.SetActive(true);
        bigHat.StartMiniGame();
    }

    public void EndMiniGame()
    {
        bigHat.EndMiniGame();
    }
    private void CheckIfMiniGamePassed()
    {
        if (collectiblesPickedUp >= _numberCollectibleToPass)
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

    public void GetHat()
    {
        collectiblesPickedUp++;
    }
}
