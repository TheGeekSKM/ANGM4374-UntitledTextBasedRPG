using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    GameController _controller;

    public GameSetupState GameSetupState {get; private set;}
    public GamePlayState GamePlayState {get; private set;}
    public GameDialogueState GameDialogueState {get; private set;}
    public GameExtraActionState GameExtraActionState { get; private set; }
    public GameListenState GameListenState { get; private set; }
    public GameRoomInventoryState GameRoomInventoryState { get; private set; }
    public GameInventoryState GameInventoryState { get; private set; }

    void Awake()
    {
        _controller = GetComponent<GameController>();

        GameSetupState = new GameSetupState(_controller, this);
        GamePlayState = new GamePlayState(_controller, this);
        GameDialogueState = new GameDialogueState(_controller, this);
        GameExtraActionState = new GameExtraActionState(_controller, this);
        GameListenState = new GameListenState(_controller, this);
        GameRoomInventoryState = new GameRoomInventoryState(_controller, this);
        GameInventoryState = new GameInventoryState(_controller, this);
    }

    void Start()
    {
        ChangeState(GameDialogueState, .5f);
        // ChangeState(GamePlayState, 0.5f);
    }
}
