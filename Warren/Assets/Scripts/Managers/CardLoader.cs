using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CardLoader 
{
    EntityStats Stats;

    public CardLoader(EntityStats stats)
    {
        Stats = stats;      
    }

    #region Stat Table

    Dictionary<(ModType, Stat), Action<EntityStats, float>> statTable = new() // uses the mod type and stat as a key, returns a method
    {
        // MELEE
        { (ModType.Weapon, Stat.Damage),     (p,v) => p.WeaponBehaviour.damage += (int)v }, // increases damage
        { (ModType.Weapon, Stat.Range),      (p,v) => p.WeaponBehaviour.radius += v }, // increases radius
        { (ModType.Weapon, Stat.Speed),      (p,v) => p.MeleeCooldown = Math.Max(0.1f, p.MeleeCooldown - v) }, // decreases cooldown
        { (ModType.Weapon, Stat.ElementType), (p,v) => p.WeaponBehaviour.damageTypes.Add(GetStatusEffect((int)v)) }, // adds damage type to melee

        // SPELL
        { (ModType.Spell, Stat.Damage),     (p,v) => (p as PlayerStats).SpellDamage += (int)v }, // increases spell damage
        { (ModType.Spell, Stat.Range),      (p,v) => (p as PlayerStats).SpellRadius += v }, // increases spell range
        { (ModType.Spell, Stat.Cost),       (p,v) => (p as PlayerStats).SpellCost = Math.Max(1, (p as PlayerStats).SpellCost - (int)v) }, // decreases spell speed
        { (ModType.Spell, Stat.ElementType), (p,v) => (p as PlayerStats).SpellDamageTypes.Add(GetStatusEffect((int)v)) }, // adds damage type to spell

        // ENTITY
        //{ (ModType.Player, Stat.CurrentHealth),  (p,v) => p.CurrentHealth += (int)v },
        { (ModType.Entity, Stat.MaxHealth), (p,v) => p.MaxHealth += (int)v },
        { (ModType.Entity, Stat.Speed), (p,v) => p.MaxSpeed += v },
        { (ModType.Entity, Stat.Acceleration), (p,v) => p.Acceleration += v },
        { (ModType.Entity, Stat.ElementType), (p,v) => p.ElementType.Add(GetStatusEffect((int)v)) }, // adds damage resistance
    };


    #endregion

    public void LoadPlayerCards(List<Card> Cards)
    {

        AnimatorOverrideController animatorOverride = new(Stats.Animator.runtimeAnimatorController);  

        foreach (var card in Cards)
        {    

            if (card is WeaponCard weaponCard) { LoadWeaponCard(weaponCard, Stats, Stats.WeaponSlot); }

            else if (card is ModifierCard modifierCard) { LoadModCard(modifierCard, Stats); }

            else if (card is SpellCard spellCard) { LoadSpellCard(spellCard, Stats as PlayerStats, animatorOverride); }
            
            //other card types go here
        }
        
    }

    #region Load Player Cards
    private void LoadWeaponCard(WeaponCard weaponCard, EntityStats player, SpriteRenderer weaponSlot) // loads weapon Cards and apply their stats 
    {
        switch (weaponCard.Behaviour)
        {
            case WeaponBehaviours.Melee:
                MeleeBehaviour melee = new MeleeBehaviour();
                MeleeCard meleeCard = weaponCard as MeleeCard;

                melee.damage = meleeCard.BaseDamage;
                melee.radius = meleeCard.BaseAttackRadius;

                melee.pos = player.AttackPoint;
                player.MeleeCooldown = meleeCard.BaseAttackCooldown;
                melee.mask = player.Damageable;

                player.WeaponBehaviour = melee;
                break;

            case WeaponBehaviours.Spawn:
                SpawnBehaviour spawn = new SpawnBehaviour();
                SpawnCard spawnCard = weaponCard as SpawnCard;

                spawn.damage = spawnCard.BaseDamage;
                spawn.radius = spawnCard.BaseAttackRadius;
                spawn.pos = player.AttackPoint;

                spawn.ToSpawn = spawnCard.ToSpawn;
                break;

        }

        
        weaponSlot.sprite = weaponCard.WeaponSprite;  
    }

    private void LoadModCard(ModifierCard card, EntityStats player) // loads modification cards 
    {
        foreach (var mod in card.StatModifier) // loops through each modifier
        {
            if (statTable.TryGetValue((card.ModType, mod.Stat), out var action)) // uses the card mod target and stat to return a method
            {
                //Debug.Break();
                action(player, mod.Modifier); // calls the returned method
            }
        }
    }

    private void LoadSpellCard(SpellCard spellCard, PlayerStats player, AnimatorOverrideController overrideController)
    {
        player.SpellDamage = spellCard.BaseDamage;
        player.SpellCost = spellCard.BaseCost;
        player.SpellRadius = spellCard.BaseRadius;
        player.SpellLength = spellCard.SpellLength;

        //player.SpellBehaviour = spellCard.Behaviour;
        overrideController["BlankSpell"] = spellCard.Animation;
        Stats.Animator.runtimeAnimatorController = overrideController;
    }

    private static StatusEffect GetStatusEffect(int effect)
    {
        switch(effect)
        {
            case 1:
                return CardManager.Instance.AllStatusEffects[0]; // burning
            case 2:
                 return CardManager.Instance.AllStatusEffects[1]; // poison
            case 3:
                 return CardManager.Instance.AllStatusEffects[2]; // lightning
        }
        return null;
    }

    

    #endregion

    //public void LoadWorldCards(List<Card> Cards) { } //will eventually do stuff
}
