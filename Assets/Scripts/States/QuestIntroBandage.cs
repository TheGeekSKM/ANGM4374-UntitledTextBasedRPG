using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIntroBandage : State
{
    QuestController _controller;
    QuestFSM _fsm;

    public QuestIntroBandage(QuestController controller, QuestFSM fsm)
    {
        _controller = controller;
        _fsm = fsm;
    }

    public override void Enter()
    {
        _controller.QuestIntroBandageStarted();
    }

    public override void Exit()
    {
        
    }
}
