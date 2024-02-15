using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayState : State
{
    MainMenuFSM mainMenuFSM;
    MainMenuController mainMenuController;

    public MenuPlayState(MainMenuController controller, MainMenuFSM fsm)
    {
        mainMenuController = controller;
        mainMenuFSM = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        mainMenuController.LoadPlayScene();
        // UI Animations
    }

}
