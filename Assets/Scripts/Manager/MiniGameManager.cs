using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { NONE, FIRSTMG, SECONDMG, THIRDMG, FOURTHMG, TUTO, DEAD, TRANSITION}
public class MiniGameManager : MonoBehaviour
{

    public static MiniGameManager instance;

    public State state;
    public State previousState;
    public System.Action onChangeState;


    [Header("Mini-game 1")]
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

    public void ChangeState(State newState)
    {
        previousState = state;
        state = newState;
        if (onChangeState != null) onChangeState.Invoke();
    }
}
