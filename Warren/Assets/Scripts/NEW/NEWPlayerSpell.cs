using UnityEngine;
using System.Collections.Generic;

public class NEWPlayerSpell : MonoBehaviour
{
    public static List<NEWSpell> PlayerSpells = new();

    public void OnInitialized()
    {
        foreach (var spell in PlayerSpells) { spell.OnInitialize(); }
    }

    public static List<T> GetSpellsOfType<T>() where T : class
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

    // TO DO
    // Handle loading spells and create a card debug menu
}
