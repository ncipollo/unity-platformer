using UnityEngine;

[System.Serializable]
public class Stat {
    private float current;

    public Stat(
        float current = 0,
        float delta = 0,
        float min = 0,
        float max = 0
    ) {
        this.current = current;
        Min = min;
        Max = max;
        Delta = delta;
    }

    public float Current { 
        get { return current; } 

        set { 
            
        }
    }

    public float Delta { get; }

    public float Min { get; }

    public float Max { get; }
}