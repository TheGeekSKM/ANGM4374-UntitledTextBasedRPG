using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MainMenuFSM : StateMachineMB
{
    MainMenuController _controller;

    public MainMenuState MainMenuState {get; private set;}
    public MenuCreditsState MenuCreditsState {get; private set;}
    public MenuPlayState MenuPlayState {get; private set;}

    void Awake()
    {
        _controller = GetComponent<MainMenuController>();

        MainMenuState = new MainMenuState(_controller, this);
        MenuCreditsState = new MenuCreditsState(_controller, this);
        MenuPlayState = new MenuPlayState(_controller, this);

    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        ChangeState(MainMenuState);
    }

    public void Credits()
    {
        ChangeState(MenuCreditsState, 1f);
    }

    public void MainMenu()
    {
        ChangeState(MainMenuState, 1f);
    }

    public void Play()
    {
        ChangeState(MenuPlayState, 1f);
    }
}
