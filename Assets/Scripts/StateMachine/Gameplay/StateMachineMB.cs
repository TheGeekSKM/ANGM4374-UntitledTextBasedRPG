using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineMB : MonoBehaviour
{
    public State CurrentState {get; private set;}
    State _previousState;

    bool _inTransition = false;


    public void ChangeState(State newState)
    {
        if (CurrentState == newState || _inTransition)
            return;

        ChangeStateSequence(newState);
    }

    public void ChangeState(State newState, float delay)
    {
        StartCoroutine(ChangeStateWithDelay(newState, delay));
    }
    IEnumerator ChangeStateWithDelay(State newState, float delay)
    {
        _inTransition = true;

        CurrentState?.Exit();
        StoreStateAsPrevious(newState);

        yield return new WaitForSeconds(delay);

        CurrentState = newState;

        CurrentState?.Enter();
        _inTransition = false;
    }

    void ChangeStateSequence(State newState)
    {
        _inTransition = true;

        CurrentState?.Exit();
        StoreStateAsPrevious(newState);

        CurrentState = newState;

        CurrentState?.Enter();
        _inTransition = false;
    }


    void StoreStateAsPrevious(State newState)
    {
        //first time
        if (_previousState == null && newState != null) _previousState = newState;
        //not first time
        else if (_previousState != null && CurrentState != null) _previousState = CurrentState;
    }

    public void ChangeStateToPrevious()
    {
        if (_previousState == null) 
        {
            Debug.LogWarning("Previous state is null");
            return;
        }

        ChangeState(_previousState);
    }


    void Update()
    {
        if (CurrentState != null && !_inTransition) CurrentState?.Tick();
    }

    void FixedUpdate()
    {
        if (CurrentState != null && !_inTransition) CurrentState?.FixedTick();
    }

    protected virtual void OnDestroy()
    {
        CurrentState?.Exit();
    }

}
