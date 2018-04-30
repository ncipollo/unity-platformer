using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;

public class StaminaRegenEffectTest {
    private IStatusEffect statusEffect;

    [SetUp]
    public void setUp() {
        statusEffect = new StaminaRegenEffect();
    }

    [Test]
    public void Apply() {
        var stats = new Stats();
        stats.SetStamina(0);

        statusEffect.Apply(stats, 1.0f);

        Assert.AreEqual(stats.staminaRegenRate, stats.stamina);
    }

    [Test]
    public void Completed() {
        Assert.False(statusEffect.Completed);
    }

    [Test]
    public void VisualEffects() {
        Assert.AreEqual(0, statusEffect.VisualEffects.Count);
    }
}