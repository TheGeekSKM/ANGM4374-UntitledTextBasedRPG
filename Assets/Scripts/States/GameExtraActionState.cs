using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExtraActionState : State
{
    GameController _controller;
    GameFSM _gameFSM;

    public GameExtraActionState(GameController gC, GameFSM gF)
    {
        _controller = gC;
        _gameFSM = gF;
    }

    public override void Enter()
    {
        base.Enter();
        _controller.ExtraActionsIntro();
    }

    public override void Exit()
    {
        base.Exit();
        _controller.ExtraActionsOutro();
    }

}
