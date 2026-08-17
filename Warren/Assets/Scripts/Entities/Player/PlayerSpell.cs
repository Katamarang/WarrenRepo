using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpell : MonoBehaviour
{
    static List<Spell> PlayerSpells = new();
    [SerializeField] Transform spellContainer;

    public static UnityAction UpdateValues;

    private void Start()
    {
        UpdateValues?.Invoke();
    }

    public virtual void Initialise()
    {
        foreach (var spell in PlayerSpells) { spell.Initialised(this); }
        UpdateValues?.Invoke();
    }

    public static List<T> GetSpellsOfType<T>(ModType type) where T : class
    {
        List<T> list = new List<T>();
        foreach (var spell in PlayerSpells)
        {
            if (spell is T && spell is ModifierSpell mod && mod.modType == type)
            {
                list.Add(spell as T);
            }
        }

        return list;
    }

    public static float AdjustValue<i>(float initialValue, Func<i, float> function, ModType type) where i : class 
    {
        float temp = initialValue;
        foreach (i spell in GetSpellsOfType<i>(type))
        {
          
            temp += function(spell);
            
        }

        temp = Mathf.Clamp(temp, 0.1f, float.MaxValue);
        return temp;
    } 

    public void ApplySpells(List<Spell> spells)
    {
        ResetSpells();

        foreach (var spell in spells)
        {
            PlayerSpells.Add(Instantiate(spell, spellContainer));
        }

        Initialise();
    }

    public void ResetSpells()
    {
        PlayerSpells.Clear();

        for (int i = 0; i < spellContainer.childCount; i++)
        {
            Destroy(spellContainer.GetChild(i).gameObject);
        }
    }
}

public interface IVariableValues { public void UpdateValues(); }
