using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDialogueState : State
{
    GameController _gameController;
    GameFSM _gameFSM;

    public GameDialogueState(GameController gameController, GameFSM gameFSM)
    {
        _gameController = gameController;
        _gameFSM = gameFSM;
    }

    public override void Enter()
    {
        // Debug.Log("Game Dialogue State");
        _gameController.AnimateDialoguePanelIntro();
    }

    public override void Exit()
    {
        _gameController.AnimateDialoguePanelOutro();
    }
    
}
