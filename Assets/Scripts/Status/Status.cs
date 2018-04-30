using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Status {
    public Stats stats = new Stats();

    private List<IStatusEffect> statusEffects = new List<IStatusEffect>();

    public void AddStatusEffect(IStatusEffect statusEffect) {
        statusEffects.Add(statusEffect);
    }

    public void Update(float timeDelta) {
        foreach (var effect in statusEffects) {
            effect.Apply(stats, timeDelta);
        }
        PruneCompletedStatusEffects();
    }

    private void PruneCompletedStatusEffects() {
        statusEffects.RemoveAll(effect => effect.Completed);
    }
}