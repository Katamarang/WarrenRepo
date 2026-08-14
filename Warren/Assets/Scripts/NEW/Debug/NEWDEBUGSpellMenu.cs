using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class NEWDEBUGSpellMenu : MonoBehaviour
{
    [SerializeField] List<NEWSpell> AllSpells = new List<NEWSpell>();
    List<NEWSpell> playerSpells = new List<NEWSpell>();
    [SerializeField] NEWPlayerSpell Player;

    [Header("Menu")]
    [SerializeField] NEWDEBUGSpellButton Button;
    [SerializeField] Transform Container;

    private void Start()
    {
        foreach (var spell in AllSpells)
        {
            NEWDEBUGSpellButton spellButton = Instantiate(Button, Container);
            spellButton.DisplaySpell(spell, this);
        }
    }

    public void AddSpell(NEWSpell spell) => playerSpells.Add(spell);
    public void RemoveSpell(NEWSpell spell)
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
