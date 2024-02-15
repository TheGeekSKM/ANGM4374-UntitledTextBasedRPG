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
}
