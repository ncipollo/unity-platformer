using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls {
    public string Horizontal { get; }
	public string Jump { get; }
	public string Fire1 { get; }
	public string Fire2 { get; }

    public PlayerControls(string playerSuffix) {
		Horizontal = $"Horizontal{playerSuffix}";
		Jump = $"Jump{playerSuffix}";
		Fire1 = $"Fire1{playerSuffix}";
		Fire2 = $"Fire2{playerSuffix}";
    }
}
