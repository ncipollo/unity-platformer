using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public class PlayerMovementTest {
    private const float walkForce = 1f;
    private const float maxWalkSpeed = 1f;

    private GameObject gameObject;
    private PlayerMovement playerMovement;
    private Rigidbody2D rigidbody2D;
    private IPlayerAnimator playerAnimator;

    [SetUp]
    public void SetUp() {
        SetupAnimtor();
        SetupGameObject();
        SetupPlayerMovement();
    }

    void SetupAnimtor() {
        playerAnimator = Substitute.For<IPlayerAnimator>();
    }

    void SetupGameObject() {
        gameObject = new GameObject();
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<Animator>();

        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void SetupPlayerMovement() {
        playerMovement = new PlayerMovement(
            playerAnimator: playerAnimator,
            playerRigidBody: rigidbody2D,
            playerTransform: gameObject.transform,
            walkForce: walkForce,
            maxWalkSpeed: maxWalkSpeed);
    }

    [UnityTest]
    public IEnumerator Walking_MovesRight() {
        playerMovement.CheckHorizontalAxis(1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.Greater(gameObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator Walking_MovesLetft() {
        playerMovement.CheckHorizontalAxis(-1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.Less(gameObject.transform.position.x, 0);
    }

    [UnityTest]
    public IEnumerator Walk_Flip_StartingRight() {
        playerMovement.CheckHorizontalAxis(-1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(-1, gameObject.transform.localScale.x);
    }

    [UnityTest]
    public IEnumerator Walk_Flip_StartingLeft() {
        playerMovement.CheckHorizontalAxis(-1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();
        playerMovement.CheckHorizontalAxis(1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(1, gameObject.transform.localScale.x);
    }

    [UnityTest]
    public IEnumerator Walk_SetsAnimationToWalk() {
        playerMovement.CheckHorizontalAxis(1f);
        playerMovement.Update();
        yield return new WaitForFixedUpdate();

        playerAnimator
		.Received()
        .UpdateAnimationState(PlayerAnimationState.WALKING);
    }
}
