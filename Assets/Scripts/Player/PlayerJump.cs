using UnityEngine;

public class PlayerJump {
    private PlayerMotionConstants motionConstants;

    private bool? grounded = null;
    private bool jumping;
    private int jumpsRemaining = 0;
    private bool jumpButton;
    private float jumpTimeLeft = 0;

    public PlayerJump(PlayerMotionConstants motionConstants) {
        this.motionConstants = motionConstants;
        jumpTimeLeft = motionConstants.jumpTime;
        jumpsRemaining = motionConstants.maxJumps;
    }

    public Vector2 ApplyVelocity(Vector2 initial, float deltaTime) {
        var velocity = initial;
        if (jumpTimeLeft >= 0 && jumping) {
            velocity.y = motionConstants.jumpSpeed;
            jumpTimeLeft -= deltaTime;
        }
        return velocity;
    }

    public void Update(bool grounded, bool jumpButton) {
        UpdateRemainingJumps(grounded, jumpButton);

        if (jumpButton) {
            if (jumpsRemaining > 0) {
                jumping = true;
            }
        } else {
            jumping = false;
            jumpTimeLeft = motionConstants.jumpTime;
            if (grounded) {
                jumpsRemaining = motionConstants.maxJumps;
            }
        }

        this.jumpButton = jumpButton;
        this.grounded = grounded;
    }

    void UpdateRemainingJumps(bool grounded, bool jumpButton) {
        var movedOffGround =
            (GroundedChanged(grounded) && !grounded && !jumping);
        var jumpButtonReleased =
            (JumpButtonChanged(jumpButton) && !jumpButton);
        var shouldUpdate = movedOffGround || jumpButtonReleased;
        
        if(shouldUpdate) {
            jumpsRemaining--;
        }
    }

    bool JumpButtonChanged(bool jumpButton) {
        return this.jumpButton != jumpButton;
    }

    bool GroundedChanged(bool grounded) {
        return this.grounded != grounded;
    }
}
