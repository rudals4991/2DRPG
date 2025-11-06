using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPool : ObjectPoolBase
{
    [System.Serializable]
    public class PlayerType
    {
        public CharacterData data;
        public GameObject prefab;
    }

    [SerializeField] List<PlayerType> playerTypes = new();
    readonly Dictionary<string, Queue<GameObject>> poolDic = new(); // key = CharacterData.Name
    readonly List<(string name, GameObject obj)> activeObjDic = new();
    int myPoolSize = 3;

    protected override void Awake()
    {
        DIContainer.Register(this as PlayerPool);
        SetPoolSize(myPoolSize);
    }
    public override void Initialize()
    {
        foreach (var type in playerTypes)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(type.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDic[type.data.Name] = queue;
        }
    }

    public GameObject Get(CharacterData data, Vector3 pos, Quaternion rot)
    {
        if (!poolDic.ContainsKey(data.Name)) return null;

        // 이미 해당 타입이 활성화되어 있으면 중복 소환 방지
        if (activeObjDic.Any(x => x.name == data.Name))
        {
            Debug.Log($"[PlayerPool] {data.Name}은 이미 활성화 상태입니다.");
            return null;
        }

        var queue = poolDic[data.Name];
        GameObject obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(GetPrefab(data.Name), transform);

        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        activeObjDic.Add((data.Name, obj));

        if (obj.TryGetComponent(out PlayerBase player))
        {
            player.Initialize();
        }

        return obj;
    }
    private GameObject GetPrefab(string name)
    {
        foreach (var p in playerTypes)
        {
            if (p.data != null && p.data.Name == name) return p.prefab;
        }
        return null;
    }
    public void Release(string name, GameObject obj)
    {
        obj.SetActive(false);
        activeObjDic.RemoveAll(x => x.obj == obj);

        if (poolDic.ContainsKey(name))
            poolDic[name].Enqueue(obj);
        else
            Destroy(obj);
    }
    public void Release(CharacterData data, GameObject obj)
    {
        if (data == null) return;
        Release(data.Name, obj);
    }
    public override void ReleaseAll()
    {
        foreach (var (name, obj) in activeObjDic.ToList())
        {
            Release(name, obj);
        }
        activeObjDic.Clear();
    }
}
