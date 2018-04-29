using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;

public class StatsTest {
    private Stats stats;

    [SetUp]
    public void setUp() {
        stats = new Stats();
    }

    [Test]
    public void SetHp_ClampedToMin() {
        stats.SetHP(-1);
        Assert.AreEqual(0, stats.hp);
    }

    [Test]
    public void SetHp_ClampedToMax() {
        stats.SetHP(stats.maxHp * 2);
        Assert.AreEqual(stats.maxHp, stats.hp);
    }

    [Test]
    public void SetHp_SetToValue() {
        var value = stats.maxHp / 2;
        stats.SetHP(value);
        Assert.AreEqual(value, stats.hp);
    }

    [Test]
    public void SetStamina_ClampedToMin() {
        stats.SetStamina(-1);
        Assert.AreEqual(0, stats.stamina);
    }

    [Test]
    public void SetStamina_ClampedToMax() {
        stats.SetStamina(stats.maxStamina * 2);
        Assert.AreEqual(stats.maxStamina, stats.stamina);
    }

    [Test]
    public void SetStamina_SetToValue() {
        var value = stats.maxStamina / 2;
        stats.SetStamina(value);
        Assert.AreEqual(value, stats.stamina);
    }
}