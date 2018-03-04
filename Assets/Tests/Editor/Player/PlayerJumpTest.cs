using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;

public class PlayerJumpTest {

    [Test]
    public void Jumping_FromGround() {
        new PlayerJumpRobot(1)
        .StartJumpFromGround()
        .AssertJumping();
    }

    [Test]
    public void Jumping_FromGround_ContinuesWhileButtonHeld() {
        new PlayerJumpRobot(1)
        .StartJumpFromGround()
        .StartJumpFromGround()
        .AssertJumping();
    }

    [Test]
    public void Jumping_FromGround_StopsAfterMaxTime() {
        new PlayerJumpRobot(1)
        .StartJumpFromGround()
        .HoldJumpForMaxTime()
        .StartJumpFromGround()
        .AssertNotJumping();
    }

    [Test]
    public void Jumping_FromGround_NoDoubleJump() {
        new PlayerJumpRobot(1)
        .StartJumpFromGround()
        .CompleteJumpInMidair()
        .StartJumpInMidair()
        .AssertNotJumping();
    }

    [Test]
    public void Jumping_FromGround_JumpsAgainAfterLanding() {
        new PlayerJumpRobot(1)
        .StartJumpFromGround()
        .CompleteJumpFromGround()
        .StartJumpFromGround()
        .AssertJumping();
    }

    [Test]
    public void Jumping_FromGround_WithDoubleJump() {
        new PlayerJumpRobot(2)
        .StartJumpFromGround()
        .CompleteJumpInMidair()
        .StartJumpInMidair()
        .AssertJumping();
    }

    [Test]
    public void NotJumping_WhileInMidair() {
        new PlayerJumpRobot(1)
        .StartJumpInMidair()
        .AssertNotJumping();
    }

    [Test]
    public void Jumping_WhileInMidair_WithDoubleJump() {
        new PlayerJumpRobot(2)
        .StartJumpInMidair()
        .AssertJumping();
    }

}