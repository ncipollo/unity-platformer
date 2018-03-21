using UnityEngine;

public interface IObjects {
    void Destroy(Object obj);
    Object Instantiate(Object original, Vector3 position, Quaternion rotation);
}