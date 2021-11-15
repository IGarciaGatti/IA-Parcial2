using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    IState<T> current;

    public FSM()
    {
    }

    public void SetInitialState(IState<T> initialState)
    {
        current = initialState;
        current.FinitStateMachine = this;
        current.Awake();
    }

    public void OnUpdate()
    {
        if (current != null)
            current.Execute();
    }
    public void Transition(T input)
    {
        IState<T> newState = current.GetTransition(input);
        if (newState != null)
        {
            newState.FinitStateMachine = this;
            current.Sleep();
            current = newState;
            current.Awake();
        }
    }


}
