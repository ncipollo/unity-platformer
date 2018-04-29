using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaminaRegenEffect : IStatusEffect {
    public void Apply(Stats stats, float timeDelta) {
        var stamina = stats.stamina + (stats.staminaRegenRate * timeDelta);
        stats.SetStamina(stamina);
    }

    public bool Completed {
        get { return false; }
    }

    public List<IVisualEffect> VisualEffects {
        get { return new List<IVisualEffect>(); }
    }
}