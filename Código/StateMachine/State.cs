using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> : IState<T>
{
    Dictionary<T, IState<T>> transitions = new Dictionary<T, IState<T>>();
    protected FSM<T> fsm;

    public FSM<T> FinitStateMachine { get => fsm; set => fsm = value; }

    public virtual void Awake()
    {

    }
    public virtual void Execute()
    {

    }
    public virtual void Sleep()
    {

    }
    public void AddTransition(T input, IState<T> state)
    {
        if (!transitions.ContainsKey(input))
        {
            transitions[input] = state;
        }
    }
    public void RemoveTransition(T input)
    {
        if (transitions.ContainsKey(input))
        {
            transitions.Remove(input);
        }
    }
    public void RemoveTransition(IState<T> state)
    {
        if (transitions.ContainsValue(state))
        {
            foreach (var item in transitions)
            {
                if (item.Value == state)
                {
                    transitions.Remove(item.Key);
                }
            }
        }
    }
    public IState<T> GetTransition(T input)
    {
        if (transitions.ContainsKey(input))
        {
            return transitions[input];
        }
        return null;
    }
}
