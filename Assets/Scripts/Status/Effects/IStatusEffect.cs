using UnityEngine;

public interface IStatusEffect {
    void Apply(Stats stats, float timeDelta);

    bool Completed { get; }
}