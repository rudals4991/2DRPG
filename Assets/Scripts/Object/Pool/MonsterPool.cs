using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MonsterPool : ObjectPoolBase
{
    [System.Serializable]
    public class MonsterType
    {
        public int typeIndex;
        public GameObject prefab;
    }

    [SerializeField] List<MonsterType> monsterTypes = new();
    readonly Dictionary<int, Queue<GameObject>> poolDic = new();
    readonly List<(int typeIndex, GameObject obj)> activeObjDic = new();
    int myPoolSize = 30;
    protected override void Awake()
    {
        DIContainer.Register(this as MonsterPool);
        SetPoolSize(myPoolSize);
    }
    public override void Initialize()
    {
        foreach (var type in monsterTypes)
        {
            Queue<GameObject> queue = new();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(type.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDic[type.typeIndex] = queue;
        }
    }
    public GameObject Get(int typeIndex, Vector3 pos, Quaternion rot)
    {
        if (!poolDic.ContainsKey(typeIndex)) return null;
        var queue = poolDic[typeIndex];
        GameObject obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(GetPrefab(typeIndex), transform);
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        activeObjDic.Add((typeIndex, obj));

        if (obj.TryGetComponent(out MonsterBase monster))
        {
            monster.Initialize();
        }
        return obj;
    }
    private GameObject GetPrefab(int typeIndex)
    {
        foreach (var type in monsterTypes)
        {
            if (type.typeIndex == typeIndex) return type.prefab;
        }
        return null;
    }
    public void Release(int typeIndex, GameObject obj)
    {
        obj.SetActive(false);
        activeObjDic.RemoveAll(x => x.obj == obj);

        if (poolDic.ContainsKey(typeIndex)) poolDic[typeIndex].Enqueue(obj);
        else Destroy(obj);
    }
    public override void ReleaseAll()
    {
        foreach (var (typeIndex, obj) in activeObjDic.ToList())
        {
            Release(typeIndex, obj);
        } 
        activeObjDic.Clear();
    }
}
