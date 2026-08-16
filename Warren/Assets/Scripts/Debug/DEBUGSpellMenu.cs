using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DEBUGSpellMenu : MonoBehaviour
{
    [SerializeField] List<Spell> AllSpells = new List<Spell>();
    List<Spell> playerSpells = new List<Spell>();
    [SerializeField] PlayerSpell Player;

    [Header("Menu")]
    [SerializeField] DEBUGSpellButton Button;
    [SerializeField] Transform Container;

    private void Start()
    {
        foreach (var spell in AllSpells)
        {
            DEBUGSpellButton spellButton = Instantiate(Button, Container);
            spellButton.DisplaySpell(spell, this);
        }
    }

    public void AddSpell(Spell spell) => playerSpells.Add(spell);
    public void RemoveSpell(Spell spell)
    {
        if (playerSpells.Contains(spell)) { playerSpells.Remove(spell); }
    }

    public void ApplySpells()
    {
        Player.ApplySpells(playerSpells);
        ResetSpellMenu();    
    }

    public void ClearSpells()
    {
        Player.ResetSpells();
        ResetSpellMenu();
    }

    private void ResetSpellMenu()
    {
        foreach (Toggle toggle in Container.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }

        playerSpells.Clear();
    }

}
