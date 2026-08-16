using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    AnimatorOverrideController overrideController;
    private void Start()
    {
        animator = GetComponent<Animator>();

        overrideController = new(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
    }

    public void SetAnimation(string animName, AnimationClip front, AnimationClip side, AnimationClip back)
    {
        overrideController[$"{animName}Front"] = front;
        overrideController[$"{animName}Side"] = side;
        overrideController[$"{animName}Back"] = back;
    }

    public void SetAnimBool(string animName, bool b)
    {
        animator.SetBool(animName, b);
    }

    public void SetAnimTrigger(string animName)
    {
        animator.SetTrigger(animName);
    }

    public void SetAnimPos(float x, float y)
    {
        animator.SetFloat("PosX", x);
        animator.SetFloat("PosY", y);
    }
}
