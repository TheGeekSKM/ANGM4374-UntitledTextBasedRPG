using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : State
{
    MainMenuFSM mainMenuFSM;
    MainMenuController mainMenuController;

    public MainMenuState(MainMenuController controller, MainMenuFSM fsm)
    {
        mainMenuController = controller;
        mainMenuFSM = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        mainMenuController.AnimateMenuIntro();
        // UI Animations
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.AnimateMenuOutro();
        // UI Animations
    }

    
}
