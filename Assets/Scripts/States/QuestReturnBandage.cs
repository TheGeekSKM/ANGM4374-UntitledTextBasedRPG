using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReturnBandage : State
{
    QuestController _controller;
    QuestFSM _fsm;

    public QuestReturnBandage(QuestController controller, QuestFSM fsm)
    {
        _controller = controller;
        _fsm = fsm;
    }

    public override void Enter()
    {
        _controller.QuestReturnBandageStarted();
    }

    public override void Exit()
    {
        
    }
}
