using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : StateMachineMB
{
    EnemyController _controller;

    public EnemyIdleState EnemyIdleState {get; private set;}
    public EnemyTargetState EnemyTargetState {get; private set;}
    public EnemyAttackState EnemyAttackState { get; private set; }

    void Awake()
    {
        _controller = GetComponent<EnemyController>();

        EnemyIdleState = new EnemyIdleState(_controller, this);
        EnemyTargetState = new EnemyTargetState(_controller, this);
        EnemyAttackState = new EnemyAttackState(_controller, this);
    }

    

    
}
