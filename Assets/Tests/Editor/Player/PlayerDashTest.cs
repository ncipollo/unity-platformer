using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;

public class PlayerDashTest {

    [Test]
    public void Dashing_Left() {
        new PlayerDashRobot()
        .DashLeft()
        .AssertShouldApplyForceAtRest()
        .AssertDashingLeft();
    }

    [Test]
    public void Dashing_Right() {
        new PlayerDashRobot()
        .DashRight()
        .AssertShouldApplyForceAtRest()
        .AssertDashingRight();
    }

    [Test]
    public void Dashing_ContinuesDashing() {
        new PlayerDashRobot()
        .DashRight()
        .DashRight()
        .AssertDashingRight()
        .AssertDashingRight();
    }

    [Test]
    public void Dashing_StopsForceAtLeftMax() {
        new PlayerDashRobot()
        .DashLeft()
        .AssertShouldNotApplyForceAtLeftMax();
    }

    [Test]
    public void Dashing_StopsForceAtRightMax() {
        new PlayerDashRobot()
        .DashRight()
        .AssertShouldNotApplyForceAtRightMax();
    }

    [Test]
    public void NotDashing_InMidAir() {
        new PlayerDashRobot()
        .DashInMidAir()
        .AssertNotDashing();
    }

    [Test]
    public void NotDashing_StopsDashingAfterDuration() {
        new PlayerDashRobot()
        .DashRight()
        .WaitForDashDuration()
        .AssertNotDashing();
    }

    [Test]
    public void NotDashing_UntilCoolDownCompletes() {
        new PlayerDashRobot()
        .DashRight()
        .WaitForDashDuration()
        .DashRight()
        .AssertNotDashing()
        .WaitForDashCoolDown()
        .DashRight()
        .AssertDashingRight();
    }
}