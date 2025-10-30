using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPool : ObjectPoolBase
{
    [System.Serializable]
    public class PlayerType
    {
        public CharacterType characterType;
        public GameObject prefab;
    }

    [SerializeField] List<PlayerType> playerTypes = new();
    readonly Dictionary<CharacterType, Queue<GameObject>> poolDic = new();
    readonly List<(CharacterType type, GameObject obj)> activeObjDic = new();
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
            poolDic[type.characterType] = queue;
        }
    }

    public GameObject Get(CharacterType type, Vector3 pos, Quaternion rot)
    {
        if (!poolDic.ContainsKey(type)) return null;

        // 이미 해당 타입이 활성화되어 있으면 중복 소환 방지
        if (activeObjDic.Any(x => x.type == type))
        {
            Debug.Log($"[PlayerPool] {type} 타입은 이미 활성화 상태입니다.");
            return null;
        }

        var queue = poolDic[type];
        GameObject obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(GetPrefab(type), transform);

        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        activeObjDic.Add((type, obj));

        if (obj.TryGetComponent(out PlayerBase player))
        {
            player.Initialize();
        }

        return obj;
    }
    private GameObject GetPrefab(CharacterType type)
    {
        foreach (var p in playerTypes)
        {
            if (p.characterType == type) return p.prefab;
        }
        return null;
    }
    public void Release(CharacterType type, GameObject obj)
    {
        obj.SetActive(false);
        activeObjDic.RemoveAll(x => x.obj == obj);

        if (poolDic.ContainsKey(type)) poolDic[type].Enqueue(obj);
        else Destroy(obj);
    }
    public override void ReleaseAll()
    {
        foreach (var (type, obj) in activeObjDic.ToList())
        {
            Release(type, obj);
        }
        activeObjDic.Clear();
    }
}
