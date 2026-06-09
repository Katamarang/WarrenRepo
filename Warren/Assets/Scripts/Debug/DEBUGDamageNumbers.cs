using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DEBUGDamageNumbers : MonoBehaviour
{
    // Debug script that displays damage numbers when activated
    
    [SerializeField] GameObject Text;
    [SerializeField] float DisplayLength;
    [SerializeField] Transform DisplayPoint;

    [Header("Damage Colours")]
    [SerializeField] Color BaseColour;
    [SerializeField] Color FireColour;
    [SerializeField] Color PoisonColour;
    [SerializeField] Color LightningColour;

    bool DamageNumbers = true;

    public void DamageNoActive(bool no) // Called by the toggle when the value is changed, sets whether damage numbers will be displayed or not.
    {
        DamageNumbers = no;
    }

    // Called by the damageable when damage is taken, displays the damage number and colour based on the type of damage taken.
    public void DisplayDamageNumber(int damage, DamageType type) 
    {
        if (!DamageNumbers) { return; }

        TMP_Text text = Instantiate(Text, DisplayPoint).GetComponent<TMP_Text>();
        text.color = GetColor(type);
        text.text = damage.ToString();

        StartCoroutine(HoldText(text));
    }

    // Overload of the above method for when multiple types of damage are applied at once.
    public void DisplayDamageNumber(int damage, List<DamageType> type)
    {
        if (!DamageNumbers) { return; }

        foreach (DamageType t in type) // loops type and displays a damage number for each.
        {
            TMP_Text text = Instantiate(Text, DisplayPoint).GetComponent<TMP_Text>();
            text.color = GetColor(t);
            text.text = damage.ToString();

            StartCoroutine(HoldText(text));
        }
    }

    private IEnumerator HoldText (TMP_Text text) // displays the text for a set amount of time.
    {
        yield return new WaitForSeconds(DisplayLength);
        Destroy(text.gameObject);
        StopCoroutine("HoldText");
    }

    private Color GetColor(DamageType type)
    {
        switch (type)
        {
            case DamageType.None:
                return BaseColour;
            case DamageType.Fire:
                return FireColour;
            case DamageType.Poison:
                return PoisonColour;
            case DamageType.Lightning:
                return LightningColour;
        }
        return Color.white;
    }
}
