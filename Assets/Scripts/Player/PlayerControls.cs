using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides Input control strings based upon the supplied player ID.
public class PlayerControls {
    public string Horizontal { get; }
	public string Jump { get; }
	public string Fire1 { get; }
	public string Fire2 { get; }

    public PlayerControls(string playerID) {
		Horizontal = $"Horizontal_{playerID}";
		Jump = $"Jump_{playerID}";
		Fire1 = $"Fire1_{playerID}";
		Fire2 = $"Fire2_{playerID}";
    }
}
