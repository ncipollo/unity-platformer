using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using NSubstitute;

public class PlayerEffectsRobot {
    private GameObject dashWind;
    private Transform dashEffectSpawn;
    private IObjects objects;
    private PlayerEffects playerEffects;

    public PlayerEffectsRobot() {
        dashWind = new GameObject("dash-wind");
        dashEffectSpawn = dashWind.transform;
        objects = Substitute.For<IObjects>();
        playerEffects = new PlayerEffects(
            dashWind,
            dashEffectSpawn,
            objects
        );
    }

    public PlayerEffectsRobot CreateDashWind() {
        playerEffects.CreateDashWind();

        return this;
    }

    public PlayerEffectsRobot FaceLeft() {
        playerEffects.SetDirection(false);

        return this;
    }

    public PlayerEffectsRobot FaceRight() {
        playerEffects.SetDirection(true);

        return this;
    }

    public PlayerEffectsRobot AssertDashWindLeft() {
        objects.Received().Instantiate(
            dashWind,
            dashEffectSpawn.position,
            Quaternion.Euler(0, 180, 0)
        );
    
        return this;
    }

    public PlayerEffectsRobot AssertDashWindRight() {
        objects.Received().Instantiate(
            dashWind,
            dashEffectSpawn.position,
            Quaternion.identity
        );
    
        return this;
    }
}