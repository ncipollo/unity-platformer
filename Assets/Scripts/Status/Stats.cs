using UnityEngine;

[System.Serializable]
public class Stats {
    public float hp = 100;
    public float maxHp = 100;
    public float stamina = 100;
    public float maxStamina = 100;

    public void SetHP(float hp) {
        hp = Mathf.Clamp(hp, 0, maxHp);
    }
    
    public void SetStamina(float stamina) {
        hp = Mathf.Clamp(stamina, 0, maxStamina);
    }
}