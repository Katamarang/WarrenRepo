using UnityEngine;

public class P_SpellState : IState
{
    // Player's spell state
    SM_Player _player;
    PlayerCombat _combat;
    float SpellLength;
    float _spellTime;

    WeaponCard card;

    public P_SpellState(SM_Player player, PlayerCombat combat)
    {
        _player = player;
        _combat = combat;
    }

    public override void Enter()
    {
        card = _combat.SpellCard;
        int SpellCost = _combat.SpellCostModifier + card.SpellCost;

        // spell fail if there is not enough charges or if there is no spell behaviour assigned
        if (_combat.ManaCharges < SpellCost || _combat.SpellCard == null) 
        {
            _player.TransitionTo(_player.IdleState); 
            return; 
        } 

        SpellLength = card.BaseAttackCooldown;

        _combat.ManaCharges -= SpellCost;
        card.OnFire(_combat.SpellDamageModifer, _combat.SpellStatusEffects, _combat.AttackPosition, _combat.Damageable);

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

}
