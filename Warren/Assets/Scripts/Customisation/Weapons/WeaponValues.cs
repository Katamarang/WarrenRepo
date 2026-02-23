using UnityEngine;

[CreateAssetMenu(fileName = "new weapon", menuName = "Scriptable Objects/WeaponValues")]
public class WeaponValues : ScriptableObject
{
    public string Name;
    [TextArea(6, 6)] public string Description;

    [Space(50)]
    public int Damage;
    public float AttackCooldown;
    public float ComboCooldown;
}
