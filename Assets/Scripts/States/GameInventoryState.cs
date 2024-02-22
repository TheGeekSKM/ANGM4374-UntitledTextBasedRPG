using UnityEngine;

public class GameInventoryState : State
{
    GameController gameController;
    GameFSM gameFSM;

    public GameInventoryState(GameController gameController, GameFSM gameFSM)
    {
        this.gameController = gameController;
        this.gameFSM = gameFSM;
    }

    public override void Enter()
    {
        gameController.AnimateInventoryPanelIntro();
    }

    public override void Exit()
    {
        gameController.AnimateInventoryPanelOutro();
    }
}