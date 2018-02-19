using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement {
    private IPlayerAnimator playerAnimator;
    private Rigidbody2D playerRigidBody;
	private Transform playerTransform;
    private float walkForce;
    private float maxWalkSpeed;

    private float controlsDx;
	private bool facingRight = true;

    public PlayerMovement(IPlayerAnimator playerAnimator,
    Rigidbody2D playerRigidBody,
	Transform playerTransform,
    float walkForce,
    float maxWalkSpeed) {
        this.playerAnimator = playerAnimator;
        this.playerRigidBody = playerRigidBody;
		this.playerTransform = playerTransform;
        this.walkForce = walkForce;
        this.maxWalkSpeed = maxWalkSpeed;
    }

    public void CheckJump(bool jumpButton) {

    }

    public void CheckHorizontalAxis(float dx) {
        controlsDx = dx;
    }

    public void Update() {
		UpdateWalk();
		UpdateAnimation();
    }

    void UpdateWalk() {
		ApplyWalkForce();
		CheckForFlip();
    }

	void ApplyWalkForce() {
		if (controlsDx * playerRigidBody.velocity.x < maxWalkSpeed) {
            playerRigidBody.AddForce(Vector2.right * controlsDx * walkForce);
        }

        if (Mathf.Abs(playerRigidBody.velocity.x) > maxWalkSpeed) {
			playerRigidBody.velocity = new Vector2(
                Mathf.Sign(playerRigidBody.velocity.x) * maxWalkSpeed,
                playerRigidBody.velocity.y
				);
		}
	}

	void CheckForFlip() {
		if (controlsDx > 0 && !facingRight) {
            Flip();
        } else if (controlsDx < 0 && facingRight) {
            Flip();
        }
	}

	void Flip() {
		facingRight = !facingRight;
        var localScale = playerTransform.localScale;
        localScale.x *= -1;
        playerTransform.localScale = localScale;
	}

	void UpdateAnimation() {
		if(Mathf.Abs(controlsDx) > 0) {
			playerAnimator.UpdateAnimationState(PlayerAnimationState.WALKING);
            playerAnimator.UpdateAnimationSpeed(Mathf.Abs(controlsDx));
		} else {
            playerAnimator.UpdateAnimationState(PlayerAnimationState.IDLE);
            playerAnimator.UpdateAnimationSpeed(1f);
        }
	}
}
