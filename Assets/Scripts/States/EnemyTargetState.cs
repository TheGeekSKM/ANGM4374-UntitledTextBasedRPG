using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetState : State
{
    EnemyController _controller;
    EnemyFSM _enemyFSM;

    public EnemyTargetState(EnemyController controller, EnemyFSM enemyFSM)
    {
        _controller = controller;
        _enemyFSM = enemyFSM;
    }

    public override void Enter()
    {
        _controller.EnemyTargetLogic();
    }
}
