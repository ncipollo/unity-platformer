using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : IPlayerAnimator {
    private Animator animator;

    private PlayerAnimationState animationState;

    public PlayerAnimator(Animator animator) {
        this.animator = animator;
    }

    public void UpdateAnimationState(PlayerAnimationState animationState) {
        if (this.animationState != animationState) {
			this.animationState = animationState;
			animator.SetTrigger(animationState.ToTrigger());
        }
    }
}
