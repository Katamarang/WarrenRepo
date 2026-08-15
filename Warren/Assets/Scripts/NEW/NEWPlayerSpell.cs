using System;
using System.Collections.Generic;
using UnityEngine;

public class NEWPlayerSpell : MonoBehaviour
{
    static List<NEWSpell> PlayerSpells = new();
    [SerializeField] Transform spellContainer;

    public void OnInitialized()
    {
        foreach (var spell in PlayerSpells) { spell.OnInitialize(); }
    }

    public static List<T> GetSpellsOfType<T>(ModType type) where T : class
    {
        List<T> list = new List<T>();
        foreach (var spell in PlayerSpells)
        {
            if (spell is T && spell is NEWModifierSpell mod && mod.modType == type)
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

    public void ApplySpells(List<NEWSpell> spells)
    {
        ResetSpells();

        foreach (var spell in spells)
        {
            PlayerSpells.Add(Instantiate(spell, spellContainer));
        }
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
