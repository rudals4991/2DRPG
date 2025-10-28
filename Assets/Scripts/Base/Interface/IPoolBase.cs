using UnityEngine;

public interface IPoolBase
{
    void Initialize();
    void ReleaseAll();
    GameObject Get(Vector3 pos, Quaternion rot);
    void Release(GameObject obj);
}
