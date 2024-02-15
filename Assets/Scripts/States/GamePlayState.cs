using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State
{
    GameFSM gameFSM;
    GameController gameController;

    public GamePlayState(GameController controller, GameFSM fsm)
    {
        gameController = controller;
        gameFSM = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        gameController.AnimateGamePlayPanelIntro();
    }

    public override void Exit()
    {
        gameController.AnimateGamePlayPanelOutro();
    }
}
