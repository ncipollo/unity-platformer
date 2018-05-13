using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlayerAnimator {
	void UpdateAnimationSpeed(float speed);
	void UpdateAnimationState(PlayerAnimationState animationState);
}