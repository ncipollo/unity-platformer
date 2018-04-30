using UnityEngine;

[System.Serializable]
public class Stats {
    public float hp = 100;
    public float maxHp = 100;
    public float stamina = 100;
    public float maxStamina = 100;
    public float staminaRegenRate = 5;

    public void SetHP(float hp) {
        this.hp = Mathf.Clamp(hp, 0, maxHp);
    }

    public void SetStamina(float stamina) {
        this.stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}