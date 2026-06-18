using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CardLoader 
{
    Entity Stats;

    EntityStats EntityStats;
    EntityHealth EntityHealth;
    EntityCombat EntityCombat;

    public CardLoader(Entity stats)
    {
        Stats = stats;

        EntityStats = Stats.GetComponent<EntityStats>();
        EntityHealth = Stats.GetComponent<EntityHealth>();
        EntityCombat = Stats.GetComponent<EntityCombat>();
    }

    // a stat dictionary that uses the mod type and stat as a key, and returns a method that modifies the appropriate stat.
    #region Stat Table
    Dictionary<(ModType, Stat), Action<Entity, float>> statTable = new() // uses the mod type and stat as a key, returns a method
    {
        // PRIMARY WEAPON
        { (ModType.Weapon, Stat.Damage),     (p,v) => p.GetComponent<EntityCombat>().PrimaryDamageModifer += (int)v }, // increases damage
        { (ModType.Weapon, Stat.Range),      (p,v) => p.GetComponent<EntityCombat>().PrimaryRadiusModifier += v }, // increases radius
        { (ModType.Weapon, Stat.Speed),      (p,v) => p.GetComponent<EntityCombat>().PrimaryCooldownModifier -= v }, // decreases cooldown
        { (ModType.Weapon, Stat.ElementType), (p,v) => p.GetComponent<EntityCombat>().PrimaryStatusEffects.Add(GetStatusEffect((int)v)) }, // adds damage type to melee

        // SPELL
        { (ModType.Spell, Stat.Damage),     (p,v) => p.GetComponent<PlayerCombat>().SpellDamageModifer += (int)v }, // increases spell damage
        { (ModType.Spell, Stat.Range),      (p,v) => p.GetComponent<PlayerCombat>().SpellRadiusModifier += v }, // increases spell range
        { (ModType.Spell, Stat.SpellCost),  (p,v) => p.GetComponent<PlayerCombat>().SpellCostModifier -= (int)v }, // decreases spell cost
        { (ModType.Spell, Stat.ElementType), (p,v) => p.GetComponent<PlayerCombat>().SpellStatusEffects.Add(GetStatusEffect((int)v)) }, // adds damage type to spell

        // ENTITY
        //{ (ModType.Player, Stat.CurrentHealth),  (p,v) => p.CurrentHealth += (int)v },
        { (ModType.Entity, Stat.MaxHealth), (p,v) => p.GetComponent<EntityHealth>().MaxHealth += (int)v },
        { (ModType.Entity, Stat.CurrentHealth), (p,v) => p.GetComponent<EntityHealth>().CurrentHealth += (int)v },
        { (ModType.Entity, Stat.Speed), (p,v) => p.GetComponent<EntityStats>().SpeedModifier += v },
        { (ModType.Entity, Stat.Acceleration), (p,v) => p.GetComponent<EntityStats>().Acceleration += v },
        { (ModType.Entity, Stat.StatusResistant), (p,v) => p.GetComponent<EntityHealth>().StatusResistant.Add(GetStatusEffect((int)v)) }, // adds damage resistance
        { (ModType.Entity, Stat.StatusVunerable), (p,v) => p.GetComponent<EntityHealth>().StatusVunerable.Add(GetStatusEffect((int)v)) }, // adds damage vunerability
    };


    #endregion

    public void LoadEntityCards(List<Card> Cards)
    {
        AnimatorOverrideController animatorOverride = new((Stats as EntityStats).Animator.runtimeAnimatorController);  

        foreach (var card in Cards) // loops through each card and calls the appropriate load method based on card type
        {    
            if (card is WeaponCard weaponCard) { LoadWeaponCard(weaponCard, Stats.GetComponent<EntityCombat>().WeaponSlot); }

            else if (card is ModifierCard modifierCard) { LoadModCard(modifierCard, Stats); }
            
            //other card types go here
        }
        
    }

    #region Load Player Cards

    // loads weapon Cards and apply their stats to the weapon behaviour.
    private void LoadWeaponCard(WeaponCard weaponCard, SpriteRenderer weaponSlot) 
    {
        EntityCombat combat = Stats.GetComponent<EntityCombat>();
        //Debug.Log(combat);
       
        if (weaponCard.type == WeaponType.Weapon)
        {
            combat.PrimaryCard = weaponCard;
            weaponSlot.sprite = weaponCard.WeaponSprite;
        } else if (weaponCard.type == WeaponType.Spell)
        {
            combat.GetComponent<PlayerCombat>().SpellCard = weaponCard;
        }             
    }

    // loads modification cards 
    private void LoadModCard(ModifierCard card, Entity player) 
    {
        foreach (var mod in card.StatModifier) // loops through each modifier
        {
            if (statTable.TryGetValue((card.ModType, mod.Stat), out var action)) // uses stat table to return a method
            {
                action(player, mod.Modifier); // calls the returned method
            }
        }
    }

    private static StatusEffect GetStatusEffect(int effect) // returns a status effect based on an integer.
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
