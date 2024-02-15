using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupState : State
{
    GameFSM gameFSM;
    GameController gameController;

    public GameSetupState(GameController controller, GameFSM fsm)
    {
        gameController = controller;
        gameFSM = fsm;
    }

    
}
