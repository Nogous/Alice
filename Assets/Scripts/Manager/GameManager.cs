using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool onDebugMode;

    public System.Action<int> onPhaseChange;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DialogueReader.instance.CheckDialogue("BLA", 1);

        onPhaseChange += (int stateId) =>
        {
            switch (stateId)
            {
                case 1:
                    MiniGameManager.instance.ChangeState(State.TUTO);
                    break;
                case 2:
                    DialogueReader.instance.CheckDialogue("FMG", 1);
                    break;
                case 3:
                    MiniGameManager.instance.ChangeState(State.FIRSTMG);
                    break;
                case 4:
                    DialogueReader.instance.CheckDialogue("SMG", 1);
                    break;
                case 5:
                    MiniGameManager.instance.ChangeState(State.THIRDMG);
                    break;
            }
        };

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDead()
    {
        MiniGameManager.instance.ChangeState(State.DEAD);
    }
}
