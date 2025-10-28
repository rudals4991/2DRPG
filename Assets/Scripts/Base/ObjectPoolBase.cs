using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPoolBase : MonoBehaviour
{
    [SerializeField] protected GameObject objectPrefab;
    protected readonly Queue<GameObject> pool = new();
    protected readonly List<GameObject> activeObj = new();
    protected int poolSize;

    protected virtual void Awake()
    {
        DIContainer.Register(this);
    }
    protected void SetPoolSize(int mySize)
    { 
        poolSize = mySize;
    }
    public virtual void Initialize()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public virtual GameObject Get(Vector3 pos, Quaternion rot)
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(objectPrefab, transform);
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        activeObj.Add(obj);
        return obj;
    }
    public virtual void Release(GameObject obj)
    {
        obj.SetActive(false);
        activeObj.Remove(obj);
        pool.Enqueue(obj);
    }
    public virtual void ReleaseAll()
    {
        foreach (GameObject obj in activeObj.ToList()) Release(obj);
        activeObj.Clear();
    }
}
