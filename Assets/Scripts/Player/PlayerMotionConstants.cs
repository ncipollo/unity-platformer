using UnityEngine;

[System.Serializable]
public class PlayerMotionConstants : System.Object {
    public float walkForce;
    public float maxWalkSpeed;
    public float dashForce;
    public float jumpSpeed;
    public float jumpTime;
    public int maxJumps;

    public PlayerMotionConstants(
        float walkForce = 350,
        float maxWalkSpeed = 50,
        float dashForce = 500,
        float jumpSpeed = 10,
        float jumpTime = .5f,
        int maxJumps = 1
    ) {
        this.walkForce = walkForce;
        this.maxWalkSpeed = maxWalkSpeed;
        this.dashForce = dashForce;
        this.jumpSpeed = jumpSpeed;
        this.jumpTime = jumpTime;
        this.maxJumps = maxJumps;
    }
}