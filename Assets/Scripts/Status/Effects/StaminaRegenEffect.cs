using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaminaRegenEffect : IStatusEffect {
    private const float REGEN_RATE = 5; 
    public void Apply(Stats stats, float timeDelta) {
        var stamina = stats.stamina + (REGEN_RATE * timeDelta);
        stats.SetStamina(stamina);
    }

    public bool Completed {
        get { return false; }
    }

    public List<IVisualEffect> VisualEffects {
        get { return new List<IVisualEffect>(); }
    }
}