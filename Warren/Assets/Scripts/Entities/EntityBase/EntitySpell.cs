using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntitySpell : Entity
{
    [SerializeField] List<Spell> EntitySpells = new();

    public UnityAction UpdateValues;

    private void Start()
    {
        UpdateValues?.Invoke();
    }

    public List<T> GetModifierSpellsOfType<T>(ModType type, StatType stat) where T : class
    {
        List<T> list = new List<T>();
        foreach (var spell in EntitySpells)
        {
            if ( SpellValid<T>(type, stat, spell) )
            {
                list.Add(spell as T);
            }
        }

        return list;
    }

    private  bool SpellValid<T>(ModType type, StatType stat, Spell spell) where T : class
    {
        return spell is T && 
            spell is ModifierSpell mod &&
            mod.modType == type &&
            mod.statType == stat;
    }

    public bool ContainsSpell<T>(out T spell) where T : class
    {
        foreach (var s in EntitySpells)
        {
            if (s is T) { spell = s as T; return true; }
        }

        spell = null;
        return false;
    }

    public float AdjustValue(float initialValue, ModType type, StatType stat)
    {
        float temp = initialValue;
        foreach (IAdjustValue spell in GetModifierSpellsOfType<IAdjustValue>(type, stat))
        {
            temp = spell.AdjustValue(temp);          
        }

        temp = Mathf.Clamp(temp, 0.1f, float.MaxValue);
        return temp;
    } 

    public void ApplySpells(List<Spell> spells)
    {
        ResetSpells();

        foreach (var spell in spells)
        {
            EntitySpells.Add(spell);
            spell.Initialised(this);
        }

        UpdateValues?.Invoke();
    }

    public void ResetSpells()
    {      
        foreach (Spell spell in EntitySpells)
        {
            spell.OnDisabled();
        }

        EntitySpells.Clear();
    }
}

public interface IVariableValues { public void UpdateValues(); }
