using UnityEngine;

public class P_SpellState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;

    int SpellCharges;

    public P_SpellState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;
    }

    public override void Enter()
    {
        if (SpellCharges < _playerStats.SpellCost) { _player.TransitionTo(_player.IdleState); }

        SpellCharges -= _playerStats.SpellCost;
        Debug.Log("Spell");
    }

    public override void Update()
    {
        

        _player.TransitionTo(_player.IdleState);
    }

    public override void Exit() { }

    public override void FixedUpdate() { }

    public void AddSpellCharge() { SpellCharges++; }
    public int GetSpellCharge() { return SpellCharges; }
}
