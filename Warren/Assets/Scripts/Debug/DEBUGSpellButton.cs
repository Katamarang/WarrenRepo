using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DEBUGSpellButton : MonoBehaviour
{
    Spell Spell;
    DEBUGSpellMenu Menu;
    [SerializeField] Toggle Toggle;

    [SerializeField] TMP_Text Text;

    public void DisplaySpell(Spell spell, DEBUGSpellMenu menu)
    {
        Spell = spell;
        Menu = menu;

        Text.text = spell.Name;
    }

    public void SelectSpell(bool toggle)
    {
        if (toggle)
        {
            Toggle.image.color = Color.gray;
            Menu.AddSpell(Spell);
        }
        else
        {
            Toggle.image.color = Color.white;
            Menu.RemoveSpell(Spell);
        }
    }
}
