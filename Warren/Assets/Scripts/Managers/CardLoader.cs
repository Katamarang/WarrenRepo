using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CardLoader 
{
    IStats Stats;

    public CardLoader(IStats stats)
    {
        Stats = stats;
        
    }


    #region Stat Table

    Dictionary<(ModType, Stat), Action<PlayerStats, float>> statTable = new() // uses the mod type and stat as a key, returns a method
    {
        // MELEE
        { (ModType.Weapon, Stat.Damage),     (p,v) => p.MeleeDamage += (int)v }, // increases damage
        { (ModType.Weapon, Stat.Range),      (p,v) => p.AttackRadius += v }, // increases radius
        { (ModType.Weapon, Stat.Speed),      (p,v) => p.MeleeCooldown = Math.Max(0.1f, p.MeleeCooldown - v) }, // decreases cooldown
        { (ModType.Weapon, Stat.DamageType), (p,v) => p.MeleeDamageTypes.Add((DamageType)(int)v) }, // adds damage type

        // SPELL
        { (ModType.Spell, Stat.Damage),     (p,v) => p.SpellDamage += (int)v }, // increases spell damage
        { (ModType.Spell, Stat.Range),      (p,v) => p.SpellRadius += v }, // increases spell range
        { (ModType.Spell, Stat.Cost),       (p,v) => p.SpellCost = Math.Max(1, p.SpellCost - (int)v) }, // decreases spell speed
        { (ModType.Spell, Stat.DamageType), (p,v) => p.SpellDamageTypes.Add((DamageType)(int)v) }, // adds damage type

        // PLAYER
        { (ModType.Player, Stat.CurrentHealth),  (p,v) => p.CurrentHealth += (int)v },
        { (ModType.Player, Stat.MaxHealth), (p,v) => p.MaxHealth += (int)v },
        { (ModType.Player, Stat.Speed), (p,v) => p.MaxSpeed += v },
        { (ModType.Player, Stat.Acceleration), (p,v) => p.Acceleration += v },
        { (ModType.Player, Stat.DamageType), (p,v) => p.DamageResistances.Add((DamageType)(int)v) }, // adds damage type
    };


    #endregion

    public void LoadCards(List<Card> Cards)
    {
        foreach (var card in Cards)
        {
            if (card is PlayerCard)
            {
                PlayerStats player = Stats as PlayerStats;

                if (card is WeaponCard) { LoadWeaponCard(card as WeaponCard, player); }

                else if (card is ModifierCard) { LoadModCard(card as ModifierCard, player); }

                else if (card is SpellCard) { LoadSpellCard(card as SpellCard, player); }
            }

            //other card types go here
        }
    }

    

    #region Player Cards
    private void LoadWeaponCard(WeaponCard weaponCard, PlayerStats player) // loads weapon Cards and apply their stats 
    {      
        player.MeleeDamage = weaponCard.BaseDamage;
        player.MeleeCooldown = weaponCard.BaseAttackCooldown;
        player.AttackRadius = weaponCard.BaseAttackRadius;

        player.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponCard.WeaponSprite;
        player.MeleeBehaviour = weaponCard.Behaviour;      
    }

    private void LoadModCard(ModifierCard card, PlayerStats player) // loads weapon modification cards 
    {
        foreach (var mod in card.StatModifier) // loops through each modifier and applies it
        {
            if (statTable.TryGetValue((card.ModType, mod.Stat), out var action))
            {
                //Debug.Break();
                action(player, mod.Modifier); // calls the returned method
            }
        }
    }

    private void LoadSpellCard(SpellCard spellCard, PlayerStats player)
    {
        player.SpellDamage = spellCard.BaseDamage;
        player.SpellCost = spellCard.BaseCost;
        player.SpellRadius = spellCard.BaseRadius;

        player.SpellBehaviour = spellCard.Behaviour;
    }
    #endregion
}
