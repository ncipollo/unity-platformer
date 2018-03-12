using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public class PlayerMovementTest {
    private const float PLATFORM_Y_OFFSET = 1;

    private PlayerMotionConstants motionConstants = new PlayerMotionConstants();

    private GameObject groundCheckGameObject;
    private GameObject platformGameObject;
    private GameObject playerGameObject;
    private PlayerMovement playerMovement;
    private Rigidbody2D rigidbody2D;
    private IPlayerAnimator playerAnimator;

    [SetUp]
    public void SetUp() {
        SetupAnimtor();
        SetupPlayerGameObject();
        SetupGroundCheckGameObject();
        SetupPlatformGameObject();
        SetupPlayerMovement();
    }

    [TearDown]
    public void TearDown() {
        Object.Destroy(playerGameObject);
        Object.Destroy(platformGameObject);
        Object.Destroy(groundCheckGameObject);
    }

    void SetupAnimtor() {
        playerAnimator = Substitute.For<IPlayerAnimator>();
    }

    void SetupGroundCheckGameObject() {
        groundCheckGameObject = new GameObject("Ground Check");
        groundCheckGameObject.transform.localScale = new Vector2(1, 1);

        groundCheckGameObject.transform.SetParent(playerGameObject.transform);
    }

    void SetupPlatformGameObject() {
        platformGameObject = new GameObject("Platform");
        platformGameObject.AddComponent<BoxCollider2D>();
        platformGameObject.layer = LayerMask.NameToLayer("Ground");
        platformGameObject.transform.localScale = new Vector2(1, 1);
    }

    void SetupPlayerGameObject() {
        playerGameObject = new GameObject("Player");
        playerGameObject.AddComponent<Rigidbody2D>();
        playerGameObject.AddComponent<Animator>();

        rigidbody2D = playerGameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
    }

    void SetupPlayerMovement() {
        playerMovement = new PlayerMovement(
            playerAnimator: playerAnimator,
            playerGroundCheck: groundCheckGameObject.transform,
            playerRigidBody: rigidbody2D,
            playerTransform: playerGameObject.transform,
            motionConstants: motionConstants);
    }

    [UnityTest]
    public IEnumerator Animation_Dashing() {
        playerMovement.CheckGrounded();
        playerMovement.CheckDash(true, 0f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        playerAnimator.Received()
            .UpdateAnimationState(PlayerAnimationState.DASHING);
        playerAnimator.Received().UpdateAnimationSpeed(1.0f);
    }

    [UnityTest]
    public IEnumerator Animation_Jumping() {
        playerMovement.CheckGrounded();
        playerMovement.CheckJump(true);
        playerMovement.Update();
        yield return new WaitUntil(
                () => playerGameObject.transform.position.y > 2
            );
        playerMovement.CheckGrounded();
        playerMovement.CheckJump(false);
        playerMovement.Update();

        playerAnimator.Received()
            .UpdateAnimationState(PlayerAnimationState.JUMPING);
        playerAnimator.Received().UpdateAnimationSpeed(1.0f);
    }

    [UnityTest]
    public IEnumerator Animation_Jumping_WhenFalling() {
        MovePlatformAwayFromPlayer();
        playerMovement.CheckGrounded();
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        playerAnimator.Received()
            .UpdateAnimationState(PlayerAnimationState.JUMPING);
        playerAnimator.Received().UpdateAnimationSpeed(1.0f);
    }

    void MovePlatformAwayFromPlayer() {
        platformGameObject.transform.position = new Vector2(10, 10);
    }

    [UnityTest]
    public IEnumerator Animation_Walking() {
        playerMovement.CheckHorizontalAxis(-2f);
        playerMovement.CheckGrounded();
        playerMovement.CheckJump(false);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        playerAnimator.Received()
            .UpdateAnimationState(PlayerAnimationState.WALKING);
        playerAnimator.Received().UpdateAnimationSpeed(2.0f);
    }

    [UnityTest]
    public IEnumerator Dashing_InMidAir() {
        MovePlatformAwayFromPlayer();
        playerMovement.CheckGrounded();
        playerMovement.CheckDash(true, 0);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(0, playerGameObject.transform.position.x);
    }

    [UnityTest]
    public IEnumerator Dashing_OnPlatform() {
        playerMovement.CheckGrounded();
        playerMovement.CheckDash(true, 0);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.Greater(playerGameObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator Jumping_InMidAir() {
        MovePlatformAwayFromPlayer();
        playerMovement.CheckGrounded();
        playerMovement.CheckJump(true);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(0, playerGameObject.transform.position.y);
    }

    [UnityTest]
    public IEnumerator Jumping_OnPlatform() {
        playerMovement.CheckGrounded();
        playerMovement.CheckJump(true);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.Greater(playerGameObject.transform.position.y, 0);
    }

    [UnityTest]
    public IEnumerator Walking_Flip_StartingLeft() {
        playerMovement.CheckGrounded();
        playerMovement.CheckHorizontalAxis(-1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();
        playerMovement.CheckGrounded();
        playerMovement.CheckHorizontalAxis(1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(1, playerGameObject.transform.localScale.x);
    }

    [UnityTest]
    public IEnumerator Walking_Flip_StartingRight() {
        playerMovement.CheckGrounded();
        playerMovement.CheckHorizontalAxis(-1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(-1, playerGameObject.transform.localScale.x);
    }

    [UnityTest]
    public IEnumerator Walking_MovesRight() {
        playerMovement.CheckGrounded();
        playerMovement.CheckHorizontalAxis(1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.Greater(playerGameObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator Walking_MovesLeft() {
        playerMovement.CheckGrounded();
        playerMovement.CheckHorizontalAxis(-1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.Less(playerGameObject.transform.position.x, 0);
    }
}
