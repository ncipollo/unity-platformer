using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWind : MonoBehaviour {

    public void OnAnimationEvent(string animationEvent) {
        switch (animationEvent) {
            case "end":
                Objects.shared.Destroy(gameObject);
                break;
        }
    }
}
