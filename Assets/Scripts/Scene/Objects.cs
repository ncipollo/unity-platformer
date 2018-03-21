using UnityEngine;

public class Objects : IObjects {
    private static readonly Objects instance = new Objects();

    public static Objects shared {
        get {
            return instance;
        }
    }

    private Objects() { }

    public void Destroy(Object obj) {
        Object.Destroy(obj);
    }

    public Object Instantiate(
        Object original, 
        Vector3 position, 
        Quaternion rotation
    ) {
        return Object.Instantiate(original, position, rotation);
    }
}