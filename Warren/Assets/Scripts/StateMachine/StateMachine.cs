using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // base class for all state machines. Handles transitions and state updates.
    // states themselves are responsible for their own logic.
    public IState CurrentState { get; private set; }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
    public void Update() { CurrentState?.Update(); }

    public void FixedUpdate() { CurrentState?.FixedUpdate(); }
}
