using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using NSubstitute;

public class PlayerDashRobot {
    private PlayerMotionConstants motionConstants;
    private IPlayerEffects playerEffects;
    
    private PlayerDash playerDash;

    public PlayerDashRobot() {
        motionConstants = new PlayerMotionConstants();
        playerEffects = Substitute.For<IPlayerEffects>();

        playerDash = new PlayerDash(motionConstants, playerEffects);
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
    
    public PlayerDashRobot AssertDashEffectCreated() {
        playerEffects.Received().CreateDashWind();

        return this;
    }

    public PlayerDashRobot AssertDashEffectNotCreated() {
        playerEffects.DidNotReceive().CreateDashWind();

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