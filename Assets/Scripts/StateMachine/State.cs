using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public float StateDuration {get; set;} = 0f;

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Tick()
    {
        StateDuration += Time.deltaTime;
    }

    public virtual void FixedTick()
    {

    }
}
