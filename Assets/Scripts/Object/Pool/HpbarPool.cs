using System.Collections.Generic;
using UnityEngine;

public class HpbarPool : ObjectPoolBase
{
    [SerializeField] Transform canvasTransform;
    int myPoolSize = 50;

    protected override void Awake()
    {
        DIContainer.Register(this as HpbarPool);
        SetPoolSize(myPoolSize);
    }
    public override void Initialize()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, canvasTransform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public override GameObject Get(Vector3 pos, Quaternion rot)
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(objectPrefab, canvasTransform);
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        activeObj.Add(obj);
        return obj;
    }
}
