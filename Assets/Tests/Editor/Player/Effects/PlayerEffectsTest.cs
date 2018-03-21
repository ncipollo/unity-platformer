using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerEffectsTest {
    [Test]
    public void CreateDashWind_FacingLeft() {
        new PlayerEffectsRobot()
        .FaceLeft()
        .CreateDashWind()
        .AssertDashWindLeft();
    }

    [Test]
    public void CreateDashWind_FacingRight() {
        new PlayerEffectsRobot()
        .FaceRight()
        .CreateDashWind()
        .AssertDashWindRight();
    }
}