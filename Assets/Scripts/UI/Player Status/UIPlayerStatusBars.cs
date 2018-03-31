using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatusBars : MonoBehaviour {
	public Image hpFill;
	public Image staminaFill;

	public float hp = 1.0f;
	public float stamina = 1.0f;
	
	void Update () {
		hpFill.fillAmount = hp;
		staminaFill.fillAmount = stamina;
	}
}
