using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform groundCheck;
    public PlayerMotionConstants motionConstants = new PlayerMotionConstants();
    public PlayerEffects playerEffects = new PlayerEffects();
    public string playerId = "P1";
    public Status playerStatus = new Status();

    private Animator animator;
    private Rigidbody2D rigidBody;
    private PlayerAnimator playerAnimator;
    private PlayerControls playerControls;
    private PlayerMovement playerMovement;

    void Awake() {
        SetupComponents();
        SetupPlayerObjects();
    }

    void SetupComponents() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void SetupPlayerObjects() {
        playerAnimator = new PlayerAnimator(animator);
        playerControls = new PlayerControls(playerId);
        playerMovement = new PlayerMovement(
            playerAnimator: playerAnimator,
            playerEffects: playerEffects,
            playerGroundCheck: groundCheck,
            playerRigidBody: rigidBody,
            playerTransform: transform,
            motionConstants: motionConstants
        );
    }

    void Update() {
        var jump = Input.GetAxis(playerControls.Jump) >= 1.0f;
        var dash = Input.GetButton(playerControls.Fire3);
        
        playerMovement.CheckGrounded();
        playerMovement.CheckDash(dash, Time.deltaTime);
        playerMovement.CheckJump(jump);
    }

    void FixedUpdate() {
        var dx = Input.GetAxis(playerControls.Horizontal);

        playerMovement.CheckHorizontalAxis(dx);

        playerMovement.Update();
    }
}
