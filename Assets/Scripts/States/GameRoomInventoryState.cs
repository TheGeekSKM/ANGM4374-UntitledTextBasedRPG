using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomInventoryState : State
{
    GameController gameController;
    GameFSM gameFSM;

    public GameRoomInventoryState(GameController gameController, GameFSM gameFSM)
    {
        this.gameController = gameController;
        this.gameFSM = gameFSM;
    }

    public override void Enter()
    {
        gameController.AnimateRoomInventoryPanelIntro();
    }

    public override void Exit()
    {
        gameController.AnimateRoomInventoryPanelOutro();
    }
}
