using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerControlsTest {

	private PlayerControls playerControls;

	[SetUp]
	public void SetUp() {
		playerControls = new PlayerControls("_P1");
	}

	[Test]
	public void Horizontal() {
		Assert.AreEqual("Horizontal_P1", playerControls.Horizontal);
	}

	[Test]
	public void Jump() {
		Assert.AreEqual("Jump_P1", playerControls.Jump);
	}

	[Test]
	public void Fire1() {
		Assert.AreEqual("Fire1_P1", playerControls.Fire1);
	}

	[Test]
	public void Fire2() {
		Assert.AreEqual("Fire2_P1", playerControls.Fire2);
	}
}
