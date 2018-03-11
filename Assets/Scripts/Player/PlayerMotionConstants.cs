using UnityEngine;

[System.Serializable]
public class PlayerMotionConstants : System.Object {
    public float walkForce;
    public float walkMaxSpeed;
    public float dashForce;
    public float dashDuration;
    public float dashCoolDown;
    public float dashMaxSpeed;
    public float jumpSpeed;
    public float jumpTime;
    public int maxJumps;

    public PlayerMotionConstants(
        float walkForce = 350,
        float maxWalkSpeed = 50,
        float dashForce = 500,
        float dashDuration = .5f,
        float dashCoolDown = .25f,
        float dashMaxSpeed = 200,
        float jumpSpeed = 10,
        float jumpTime = .5f,
        int maxJumps = 1
    ) {
        this.walkForce = walkForce;
        this.walkMaxSpeed = maxWalkSpeed;
        this.dashForce = dashForce;
        this.dashDuration = dashDuration;
        this.dashMaxSpeed = dashMaxSpeed;
        this.dashCoolDown = dashCoolDown;
        this.jumpSpeed = jumpSpeed;
        this.jumpTime = jumpTime;
        this.maxJumps = maxJumps;
    }
}