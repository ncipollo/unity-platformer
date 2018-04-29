using UnityEngine;
using System.Collections.Generic;

public interface IStatusEffect {
    void Apply(Stats stats, float timeDelta);

    bool Completed { get; }
    
    List<IVisualEffect> VisualEffects { get; }
}