using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFSM : MonoBehaviour
{
    MainMenuController _controller;

    public MainMenuState MainMenuState {get; private set;}

    void Awake()
    {
        _controller = GetComponent<MainMenuController>();

        MainMenuState = new MainMenuState(_controller, this);
    }

    void Start()
    {
        MainMenuState.Enter();
    }
}
