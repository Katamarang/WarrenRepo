using UnityEngine;

public abstract class IState 
{
    public abstract void Enter(); // code that runs when we first enter the state

    public abstract void Update(); // code that runs when we first enter the state

    public abstract void FixedUpdate(); // per physics update

    public abstract void Exit(); // code that runs when we exit the state

}
