using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    EnemyController _controller;
    EnemyFSM _enemyFSM;

    public EnemyIdleState(EnemyController controller, EnemyFSM enemyFSM)
    {
        _controller = controller;
        _enemyFSM = enemyFSM;
    }

    public override void Enter()
    {
        _controller.EnemyIdleLogic();
    }
}
