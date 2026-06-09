using UnityEngine;

public class P_SpellState : IState
{
    // Player's spell state
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
        // spell fail if there is not enough charges or if there is no spell behaviour assigned
        if (SpellCharges < _playerStats.SpellCost || _playerStats.SpellBehaviour == null) 
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

        _player.TransitionTo(_player.IdleState); // will transition back to idle once the spell duration ends.
    }

    public override void Exit() { }

    public override void FixedUpdate() { }

    public void AddSpellCharge() { SpellCharges++; }
    public int GetSpellCharge() { return SpellCharges; }
}
