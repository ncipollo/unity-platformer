using UnityEngine;

[System.Serializable]
public class PlayerMotionConstants : System.Object {
    public float walkForce = 350;
    public float maxWalkSpeed = 50;
    public float jumpForce = 500;
    public float dashForce = 5000;

    public PlayerMotionConstants(
        float walkForce,
        float maxWalkSpeed,
        float jumpForce,
        float dashForce
    ) {
        this.walkForce = walkForce;
        this.maxWalkSpeed = maxWalkSpeed;
        this.jumpForce = jumpForce;
        this.dashForce = dashForce;
    }
}