using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    EnemyController controller;
    EnemyFSM enemyFSM;

    public EnemyAttackState(EnemyController _controller, EnemyFSM _enemyFSM)
    {
        controller = _controller;
        enemyFSM = _enemyFSM;
    }

    public override void Enter()
    {
        controller.EnemyAttackLogic();
    }

    public override void Exit()
    {
        controller.EnemyIdleLogic();
    }
}
