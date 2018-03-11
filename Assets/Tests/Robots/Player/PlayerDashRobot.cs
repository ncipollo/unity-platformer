using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class PlayerDashRobot {
    private PlayerMotionConstants motionConstants;
    private PlayerDash playerDash;

    public PlayerDashRobot() {
        motionConstants = new PlayerMotionConstants();
        playerDash = new PlayerDash(motionConstants);
    }

    public PlayerDashRobot DashLeft() {
        playerDash.CheckDash(grounded: true, 
        dashButton: true, 
        facingRight: false, 
        deltaTime: 0f);
        
        return this;
    }

    public PlayerDashRobot DashRight() {
        playerDash.CheckDash(grounded: true, 
        dashButton: true, 
        facingRight: true, 
        deltaTime: 0f);
        
        return this;
    }

    public PlayerDashRobot DashInMidAir() {
        playerDash.CheckDash(grounded: false, 
        dashButton: true, 
        facingRight: true, 
        deltaTime: 0f);
        
        return this;
    }

    public PlayerDashRobot WaitForDashDuration() {
        playerDash.CheckDash(grounded: true, 
        dashButton: false, 
        facingRight: true, 
        deltaTime: motionConstants.dashDuration);
        
        return this;
    }

    public PlayerDashRobot WaitForDashCoolDown() {
        playerDash.CheckDash(grounded: true, 
        dashButton: false, 
        facingRight: true, 
        deltaTime: motionConstants.dashCoolDown);
        
        return this;
    }
    
    public PlayerDashRobot AssertDashingLeft() {
        Assert.AreEqual(-motionConstants.dashForce, playerDash.Force.x);
        
        return this;
    }

    public PlayerDashRobot AssertDashingRight() {
        Assert.AreEqual(motionConstants.dashForce, playerDash.Force.x);
        
        return this;
    }

    public PlayerDashRobot AssertNotDashing() {
        Assert.AreEqual(0, playerDash.Force.x);
        
        return this;
    }

    public PlayerDashRobot AssertShouldApplyForceAtRest() {
        Assert.IsTrue(playerDash.ShouldApplyForce(Vector2.zero));

        return this;
    }

    public PlayerDashRobot AssertShouldNotApplyForceAtLeftMax() {
        var velocity = Vector2.left * motionConstants.dashMaxSpeed;
        Assert.IsFalse(playerDash.ShouldApplyForce(velocity));

        return this;
    }

    public PlayerDashRobot AssertShouldNotApplyForceAtRightMax() {
        var velocity = Vector2.right * motionConstants.dashMaxSpeed;
        Assert.IsFalse(playerDash.ShouldApplyForce(velocity));

        return this;
    }

}