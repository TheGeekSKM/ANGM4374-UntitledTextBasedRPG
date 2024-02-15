using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    GameController _controller;

    public GameSetupState GameSetupState {get; private set;}
    public GamePlayState GamePlayState {get; private set;}

    void Awake()
    {
        _controller = GetComponent<GameController>();

        GameSetupState = new GameSetupState(_controller, this);
        GamePlayState = new GamePlayState(_controller, this);
    }

    void Start()
    {
        ChangeState(GameSetupState);
    }
}
