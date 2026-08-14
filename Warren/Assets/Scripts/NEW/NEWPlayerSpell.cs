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

    private static List<T> GetSpellsOfType<T>() where T : class
    {
        List<T> list = new List<T>();
        foreach (var spell in PlayerSpells)
        {
            if (spell is T)
            {
                list.Add(spell as T);
            }
        }

        return list;
    }

    public static float AdjustValue<i>(float initialValue, Func<i, float> function) where i : class 
    {
        float temp = initialValue;
        foreach (i spell in GetSpellsOfType<i>())
        {
            temp += function(spell);
        }
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
        for (int i = 0; i < spellContainer.childCount; i++)
        {
            Destroy(spellContainer.GetChild(i).gameObject);
        }
    }
}
