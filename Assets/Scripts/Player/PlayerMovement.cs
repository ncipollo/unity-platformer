using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement {
    private IPlayerAnimator playerAnimator;
    private Transform playerGroundCheck;
    private Rigidbody2D playerRigidBody;
    private Transform playerTransform;
    private PlayerMotionConstants motionConstants;
    private float controlsDx;
    private bool facingRight = true;
    private bool grounded = false;
    private PlayerJump playerJump;
    private PlayerDash playerDash;

    public PlayerMovement(
        IPlayerAnimator playerAnimator,
        Transform playerGroundCheck,
        Rigidbody2D playerRigidBody,
        Transform playerTransform,
        PlayerMotionConstants motionConstants) {
        this.playerAnimator = playerAnimator;
        this.playerGroundCheck = playerGroundCheck;
        this.playerRigidBody = playerRigidBody;
        this.playerTransform = playerTransform;
        this.motionConstants = motionConstants;

        playerJump = new PlayerJump(motionConstants);
        playerDash = new PlayerDash(motionConstants);
    }

    public void CheckGrounded() {
        var height = playerGroundCheck.localScale.y;
        var groundCheckTop = playerGroundCheck.position.y + (height / 2f);
        var origin = new Vector2(playerGroundCheck.position.x, groundCheckTop);
        grounded = Physics2D.BoxCast(
            origin: origin,
            size: playerGroundCheck.localScale,
            angle: 0,
            direction: new Vector2(0, -1),
            distance: height,
            layerMask: 1 << LayerMask.NameToLayer("Ground"));
    }

    public void CheckDash(bool dashButton, float deltaTime) {
        playerDash.CheckDash(grounded: grounded,
        dashButton: dashButton,
        facingRight: facingRight,
        deltaTime: deltaTime);
    }

    public void CheckJump(bool jumpButton) {
        playerJump.Update(grounded, jumpButton);
    }

    public void CheckHorizontalAxis(float dx) {
        controlsDx = dx;
    }

    public void Update() {
        UpdateOrientation();
        UpdateJump();
        UpdateDash();
        UpdateWalk();
        ApplyMaxSpeed();

        UpdateAnimation();
    }

    void UpdateOrientation() {
        if(playerDash.isDashing) {
            return;
        }
        
        if (controlsDx > 0 && !facingRight) {
            Flip();
        } else if (controlsDx < 0 && facingRight) {
            Flip();
        }
    }

    void UpdateWalk() {
        if(playerDash.isDashing) {
            return;
        }

        float maxWalkSpeed = motionConstants.walkMaxSpeed;
        float walkForce = motionConstants.walkForce;
        if (controlsDx * playerRigidBody.velocity.x < maxWalkSpeed) {
            playerRigidBody.AddForce(Vector2.right * controlsDx * walkForce);
        }
    }

    void Flip() {
        facingRight = !facingRight;
        var localScale = playerTransform.localScale;
        localScale.x *= -1;
        playerTransform.localScale = localScale;
    }

    void UpdateJump() {
        var velocity = playerRigidBody.velocity;
        playerRigidBody.velocity = playerJump.ApplyVelocity(
            initial: velocity,
            deltaTime: Time.deltaTime
            );
    }

    void UpdateDash() {
        if(playerDash.ShouldApplyForce(playerRigidBody.velocity)) {
            playerRigidBody.AddForce(playerDash.Force);
        }
    }

    void ApplyMaxSpeed() {
        float limit;
        if(playerDash.isDashing) {
            limit = motionConstants.dashMaxSpeed;
        } else {
            limit = motionConstants.walkMaxSpeed;;
        }

        if (Mathf.Abs(playerRigidBody.velocity.x) > limit) {
            playerRigidBody.velocity = new Vector2(
                Mathf.Sign(playerRigidBody.velocity.x) * limit,
                playerRigidBody.velocity.y
                );
        }
    }

    void UpdateAnimation() {
        if (!grounded) {
            playerAnimator.UpdateAnimationState(PlayerAnimationState.JUMPING);
            playerAnimator.UpdateAnimationSpeed(1f);
        } else if (Mathf.Abs(controlsDx) > 0) {
            playerAnimator.UpdateAnimationState(PlayerAnimationState.WALKING);
            playerAnimator.UpdateAnimationSpeed(Mathf.Abs(controlsDx));
        } else {
            playerAnimator.UpdateAnimationState(PlayerAnimationState.IDLE);
            playerAnimator.UpdateAnimationSpeed(1f);
        }
    }
}
