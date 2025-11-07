using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MonsterPool : ObjectPoolBase
{
    [System.Serializable]
    public class MonsterType
    {
        public CharacterData data;
        public GameObject prefab;
    }

    [SerializeField] List<MonsterType> monsterTypes = new();
    readonly Dictionary<CharacterData, Queue<GameObject>> poolDic = new();
    readonly List<(CharacterData data, GameObject obj)> activeObjDic = new();
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
            poolDic[type.data] = queue;
        }
    }
    public GameObject Get(CharacterData data, Vector3 pos, Quaternion rot)
    {
        if (!poolDic.ContainsKey(data)) return null;
        var queue = poolDic[data];
        GameObject obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(GetPrefab(data.Name), transform);
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        activeObjDic.Add((data, obj));

        if (obj.TryGetComponent(out MonsterBase monster))
        {
            monster.Initialize(data);
        }
        return obj;
    }
    private GameObject GetPrefab(string name)
    {
        foreach (var m in monsterTypes)
        {
            if (m.data != null && m.data.Name == name) return m.prefab;
        }
        return null;
    }
    public void Release(CharacterData data, GameObject obj)
    {
        obj.SetActive(false);
        activeObjDic.RemoveAll(x => x.obj == obj);

        if (poolDic.ContainsKey(data)) poolDic[data].Enqueue(obj);
        else Destroy(obj);
    }
    public override void ReleaseAll()
    {
        foreach (var (data, obj) in activeObjDic.ToList())
        {
            Release(data, obj);
        } 
        activeObjDic.Clear();
    }
}
