using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCreditsState : State
{
    MainMenuFSM mainMenuFSM;
    MainMenuController mainMenuController;

    public MenuCreditsState(MainMenuController controller, MainMenuFSM fsm)
    {
        mainMenuController = controller;
        mainMenuFSM = fsm;
    }

    public override void Enter()
    {
        base.Enter();
        mainMenuController.AnimateCreditsIntro();
        // UI Animations
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.AnimateCreditsOutro();
        // UI Animations
    }
}
