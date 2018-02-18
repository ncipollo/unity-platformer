using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public string playerId = "P1";

	private PlayerControls playerControls;

	// Use this for initialization
	void Start () {
		playerControls = new PlayerControls(playerId);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
