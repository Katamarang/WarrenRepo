using UnityEngine;

public class P_SpellState : IState
{
    SM_Player _player;
    PlayerStats _playerStats;

    int SpellCharges;
    float SpellLength;
    float _spellTime;

    public P_SpellState(SM_Player player, PlayerStats playerStats)
    {
        _player = player;
        _playerStats = playerStats;
    }

    public override void Enter()
    {
        if (SpellCharges < _playerStats.SpellCost || _playerStats.SpellBehaviour == null) // spell fail
        {
            _player.TransitionTo(_player.IdleState); 
            return; 
        } 

        SpellLength = _playerStats.SpellLength;

        SpellCharges -= _playerStats.SpellCost;
        _player.Animator.SetTrigger("IsSpell");
        Debug.Log("Spell");
    }

    public override void Update()
    {
        if (_spellTime < SpellLength) { _spellTime += Time.deltaTime; return; }

        _player.TransitionTo(_player.IdleState);
    }

    public override void Exit() { }

    public override void FixedUpdate() { }

    public void AddSpellCharge() { SpellCharges++; }
    public int GetSpellCharge() { return SpellCharges; }
}
