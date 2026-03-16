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

    public void LoadCards(List<Card> Cards)
    {
        foreach (var card in Cards)
        {
            if (card is PlayerCard)
            {
                PlayerStats player = Stats as PlayerStats;

                if (card is WeaponCard) { LoadWeaponCard(card as WeaponCard, player); }

                else if (card is WeaponModCard) { LoadWeaponModCard(card as WeaponModCard, player); }

                else if (card is StatModCard) { LoadStatModCard(card as StatModCard, player); }

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

    private void LoadWeaponModCard(WeaponModCard card, PlayerStats player) // loads weapon modification cards 
    {      
        foreach (WeaponMod mod in card.WeaponModifiers)
        {
            if (mod.WeaponStat is WeaponStat.Damage) { player.MeleeDamage += (int)mod.Modifier; }
            else if (mod.WeaponStat is WeaponStat.Speed)
            {
                player.MeleeCooldown -= mod.Modifier;
                if (player.MeleeCooldown < 0.1f) player.MeleeCooldown = 0.1f;
            }
            else if (mod.WeaponStat is WeaponStat.DamageType && !player.MeleeDamageTypes.Contains((DamageType)(int)mod.Modifier))
            {
                player.MeleeDamageTypes.Add((DamageType)(int)mod.Modifier);
            }
        }        
    }

    private void LoadStatModCard(StatModCard card, PlayerStats player) // loads player modifications 
    {
        foreach (ModifyStats stat in card.StatsToModify)
        {
            if (stat.Stat is PlayerGameStats.CurrentHealth) { player.CurrentHealth += stat.Modifier; }
            else if (stat.Stat is PlayerGameStats.MaxHealth) { player.MaxHealth += stat.Modifier; }
            else if (stat.Stat is PlayerGameStats.Speed) { player.MaxSpeed += stat.Modifier; }
            else if (stat.Stat is PlayerGameStats.Acceleration) { player.Acceleration += stat.Modifier; }
            else if (stat.Stat is PlayerGameStats.Resistance && !player.DamageResistances.Contains((DamageType)stat.Modifier))
            { player.DamageResistances.Add((DamageType)stat.Modifier); }

        }
    }

    private void LoadSpellCard(SpellCard spellCard, PlayerStats player)
    {
        player.SpellDamage = spellCard.BaseDamage;
        player.SpellCost = spellCard.BaseCost;
        player.SpellRadius = spellCard.BaseRadius;
    }
    #endregion
}
