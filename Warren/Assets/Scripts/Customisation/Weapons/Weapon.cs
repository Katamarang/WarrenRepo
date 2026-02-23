using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponValues Values;
    [SerializeField] Animator anim;

    public abstract void OnAttack();
    
}
