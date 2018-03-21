using UnityEngine;

[System.Serializable]
public class PlayerEffects : System.Object, IPlayerEffects {
    public GameObject dashWind;
    public Transform dashEffectSpawn;

    private IObjects objects;

    private bool facingRight = true;

    public PlayerEffects(
        GameObject dashWind = null,
        Transform dashEffectSpawn = null,
        IObjects objects = null) {
        this.dashWind = dashWind;
        this.dashEffectSpawn = dashEffectSpawn;
        if(objects != null) {
            this.objects = objects;
        } else {
            this.objects = Objects.shared;
        }
    }

    public void CreateDashWind() {
        objects.Instantiate(
            dashWind,
            dashEffectSpawn.position,
            rotation
        );
    }

    public void SetDirection(bool facingRight) {
        this.facingRight = facingRight;
    }

    private Quaternion rotation {
        get {
            if (facingRight) {
                return Quaternion.identity;
            } else {
                return Quaternion.Euler(0, 180, 0);
            }
        }
    }
}