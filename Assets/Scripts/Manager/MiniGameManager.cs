﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { NONE, FIRSTMG, SECONDMG, THIRDMG, FOURTHMG}
public class MiniGameManager : MonoBehaviour
{

    public static MiniGameManager instance;

    public State state;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
