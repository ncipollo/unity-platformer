using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using NSubstitute;

public class StatusTest {
    private const float TIME_DELTA = 1.0f;

    private Status status;
    private IStatusEffect completedEffect;
    private IStatusEffect ongoingEffect;


    [SetUp]
    public void setUp() {
        status = new Status();
        SetupEffects();
    }

    void SetupEffects() {
        completedEffect = Substitute.For<IStatusEffect>();
        completedEffect.Completed.Returns(true);

        ongoingEffect = Substitute.For<IStatusEffect>();
        ongoingEffect.Completed.Returns(false);
    }

    [Test]
    public void Update() {
        status.AddStatusEffect(completedEffect);
        status.AddStatusEffect(ongoingEffect);

        status.Update(TIME_DELTA);
        status.Update(TIME_DELTA);

        completedEffect.Received(1).Apply(status.stats, TIME_DELTA);
        ongoingEffect.Received(2).Apply(status.stats, TIME_DELTA);
    }
}