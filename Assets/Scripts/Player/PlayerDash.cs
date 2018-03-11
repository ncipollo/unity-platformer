using UnityEngine;

public class PlayerDash {
    private PlayerMotionConstants motionConstants;

    private bool dashButton;
    private bool dashingRight;
    private float dashTime;
    private float coolDownTime;
    private State state;

    public bool isDashing {
        get {
            return state == State.DASHING;
        }
    }

    public PlayerDash(PlayerMotionConstants motionConstants) {
        this.motionConstants = motionConstants;
    }

    public void CheckDash(bool grounded,
    bool dashButton,
    bool facingRight,
    float deltaTime) {
        switch (state) {
            case State.IDLE:
                if (grounded && (DashButtonChanged(dashButton) && dashButton)) {
                    state = State.DASHING;
                    dashTime = motionConstants.dashDuration;
                    dashingRight = facingRight;
                }
                break;
            case State.DASHING:
                dashTime -= deltaTime;
                if (dashTime <= 0) {
                    state = State.COOL_DOWN;
                    coolDownTime = motionConstants.dashCoolDown;
                }
                break;
            case State.COOL_DOWN:
                coolDownTime -= deltaTime;
                if (dashTime <= 0) {
                    state = State.IDLE;
                }
                break;
        }

        this.dashButton = dashButton;
    }

    bool DashButtonChanged(bool dashButton) {
        return this.dashButton != dashButton;
    }

    public Vector2 Force {
        get {
            if (isDashing) {
                if(dashingRight) {
                    return Vector2.right * motionConstants.dashForce;
                } else {
                    return Vector2.left * motionConstants.dashForce;
                }
            } else {
                return Vector2.zero;
            }
        }
    }

    public bool ShouldApplyForce(Vector2 velocity) {
        if(isDashing) {
            if(Mathf.Abs(velocity.x) < motionConstants.dashMaxSpeed) {
                return true;
            }
        }
        return false;
    }

    enum State {
        IDLE,
        DASHING,
        COOL_DOWN
    }
}