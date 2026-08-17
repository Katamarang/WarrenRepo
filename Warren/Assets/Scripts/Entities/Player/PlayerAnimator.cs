using System;
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

    public void SetAnimation(string animName, DirectionalAnim clips)
    {
        overrideController[$"{animName}Front"] = clips.Front;
        if (clips.Side) overrideController[$"{animName}Side"] = clips.Side;
        if (clips.Back) overrideController[$"{animName}Back"] = clips.Back;
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

[Serializable]
public struct DirectionalAnim
{
    public AnimationClip Front;
    public AnimationClip Side;
    public AnimationClip Back;

    public DirectionalAnim(AnimationClip frontClip, AnimationClip sideClip = null, AnimationClip backClip = null)
    {
        Front = frontClip;
        Side = sideClip;
        Back = backClip;
    }
}
