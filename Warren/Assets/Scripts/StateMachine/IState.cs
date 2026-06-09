using UnityEngine;

public abstract class IState 
{
    // interface for states.
    // Each state is responsible for its own logic, and the state machine is responsible for transitions and updates.
    public abstract void Enter(); // code that runs when we first enter the state

    public abstract void Update(); // code that runs when we first enter the state

    public abstract void FixedUpdate(); // per physics update

    public abstract void Exit(); // code that runs when we exit the state

}
