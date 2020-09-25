using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool onDebugMode;

    public System.Action<int> onPhaseChange;
    [SerializeField] private GameObject _teleporter;
    private GameObject _teleporterInScene;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DialogueReader.instance.CheckDialogue("UITUTO", 1);

        onPhaseChange += (int stateId) =>
        {
            switch (stateId)
            {
                case 1:
                    MiniGameManager.instance.ChangeState(State.TUTO);
                    break;
                case 2:
                    DialogueReader.instance.CheckDialogue("UICAT1", 1);
                    break;
                case 3:
                    MiniGameManager.instance.ChangeState(State.TRANSITION);
                    break;
                case 4:
                    DialogueReader.instance.CheckDialogue("UICAT2", 1);
                    break;
                case 5:
                    MiniGameManager.instance.ChangeState(State.SECONDMG);
                    break;
                case 6:
                    DialogueReader.instance.CheckDialogue("UICAT3", 1);
                    break;
                case 7:
                    MiniGameManager.instance.ChangeState(State.THIRDMG);
                    break;
                case 8:
                    DialogueReader.instance.CheckDialogue("FMG", 1);
                    break;
            }
        };

        MiniGameManager.instance.onChangeState += () =>
        {
            if(MiniGameManager.instance.state == State.NONE)
            {
                _teleporterInScene = Instantiate(_teleporter, PlayerEntity.instance.transform.position + new Vector3(0, 0, 3), Quaternion.identity);
            }
            else
            {
                Destroy(_teleporterInScene);
            }
        };

        StartCoroutine(WaitAndInstantiateTeleporter());

    }

    private IEnumerator WaitAndInstantiateTeleporter()
    {
        yield return new WaitForSeconds(0.3f);
        _teleporterInScene = Instantiate(_teleporter, PlayerEntity.instance.transform.position + new Vector3(0, 0, 3), Quaternion.identity);
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
