using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMiniGames : MonoBehaviour
{

    [SerializeField] private int _miniGameToEnd = 1;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("stop");
            switch (_miniGameToEnd)
            {
                case 1:
                    if (MiniGameManager.instance.state == State.TUTO)
                    {
                        StartCoroutine(ObjectSpawner.instance.WaitAndEndTuto());
                        print("it is stop");
                    }
                    break;
            }
        }
    }
}
