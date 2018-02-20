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
    private bool shouldJump = false;

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
    }

    public void CheckJump(bool jumpButton) {
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

        shouldJump = jumpButton && grounded;
    }

    public void CheckHorizontalAxis(float dx) {
        controlsDx = dx;
    }

    public void Update() {
        UpdateOrientation();
        UpdateWalk();
        UpdateJump();

        UpdateAnimation();
    }

    void UpdateWalk() {
        float maxWalkSpeed = motionConstants.maxWalkSpeed;
        float walkForce = motionConstants.walkForce;
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

    void UpdateOrientation() {
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

    void UpdateJump() {
        if (shouldJump) {
            playerRigidBody.AddForce(
                new Vector2(0f, motionConstants.jumpForce)
                );
            shouldJump = false;
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
