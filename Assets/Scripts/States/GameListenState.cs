using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListenState : State
{
    GameFSM gameFSM;
    GameController gameController;

    public GameListenState(GameController controller, GameFSM fsm)
    {
        gameController = controller;
        gameFSM = fsm;
    }

    public override void Enter()
    {
        Debug.Log("GameListenState Enter");
        gameController.AnimateGameListenPanelIntro();
    }

    public override void Exit()
    {
        gameController.AnimateGameListenPanelOutro();
    }
}
