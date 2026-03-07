using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponValues Values;
    public Animator Anim;

    public abstract void OnAttack();
    
}
