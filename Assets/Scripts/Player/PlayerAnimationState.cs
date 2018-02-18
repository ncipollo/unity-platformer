using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum PlayerAnimationState {
    IDLE,
    WALKING,
    JUMPING,
    DASHING
}

public static class PlayerAnimationStateExtentions {
    public static string ToTrigger(this PlayerAnimationState state) {
        switch (state) {
            case PlayerAnimationState.IDLE:
                return "idle";
            case PlayerAnimationState.WALKING:
                return "walk";
            case PlayerAnimationState.JUMPING:
                return "jump";
            case PlayerAnimationState.DASHING:
                return "dash";
            default:
                throw new ArgumentException(
					$"Unhandled state: {state.ToString()}"
					);
        }
    }
}