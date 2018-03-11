using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
public class PlayerJumpRobot {
    private Vector2 initialVelocity = new Vector2(1, 1);
    private PlayerMotionConstants motionConstants;
    private PlayerJump playerJump;

    public PlayerJumpRobot(int maxJumps) {
        motionConstants = new PlayerMotionConstants(maxJumps: maxJumps);
        playerJump = new PlayerJump(motionConstants);
    }

    public PlayerJumpRobot StartJumpFromGround() {
        playerJump.Update(true, true);
        return this;
    }

    public PlayerJumpRobot CompleteJumpFromGround() {
        playerJump.Update(true, false);
        return this;
    }

    public PlayerJumpRobot StartJumpInMidair() {
        playerJump.Update(false, true);
        return this;
    }

    public PlayerJumpRobot CompleteJumpInMidair() {
        playerJump.Update(false, false);
        return this;
    }

    public PlayerJumpRobot HoldJumpForMaxTime() {
        playerJump.ApplyVelocity(initialVelocity, motionConstants.jumpTime + 1);

        return this;
    }

    public PlayerJumpRobot AssertJumping() {
        var actual = playerJump.ApplyVelocity(initialVelocity, 0);
        var expected = new Vector2(
            initialVelocity.x,
            motionConstants.jumpSpeed
            );
        Assert.AreEqual(expected, actual);
        return this;
    }

    public PlayerJumpRobot AssertNotJumping() {
        var actual = playerJump.ApplyVelocity(initialVelocity, 0);
        var expected = initialVelocity;
        Assert.AreEqual(expected, actual);
        return this;
    }
}