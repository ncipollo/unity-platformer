using UnityEngine;

public interface StatusEffect {
    void Apply(Stats stats, float timeDelta);

    bool Completed { get; }
}