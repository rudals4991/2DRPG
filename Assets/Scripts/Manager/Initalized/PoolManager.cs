using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour, IManagerBase
{
    List<ObjectPoolBase> poolList = new();
    [SerializeField] ObjectPoolBase[] allPool;
    public int Priority => 1;
    public PlayerPool PlayerPool { get; private set; }
    public MonsterPool MonsterPool { get; private set; }

    public void Exit()
    {
        ReleaseAll();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        foreach (ObjectPoolBase pool in allPool)
        {
            pool.Initialize();
            poolList.Add(pool);
        }
        PlayerPool = poolList.OfType<PlayerPool>().FirstOrDefault();
        MonsterPool = poolList.OfType<MonsterPool>().FirstOrDefault();
    }
    public GameObject SpawnPlayer(CharacterType type, Vector3 pos, Quaternion rot)
    {
        if (PlayerPool == null) return null;
        return PlayerPool.Get(type, pos, rot);
    }

    public GameObject SpawnMonster(int typeIndex, Vector3 pos, Quaternion rot)
    {
        if (MonsterPool == null) return null;
        return MonsterPool.Get(typeIndex, pos, rot);
    }
    public void ReleaseAll()
    {
        foreach (ObjectPoolBase pool in allPool)
        {
            pool.ReleaseAll();
        }
    }
    public void ReleaseAllPlayers() => PlayerPool?.ReleaseAll();
    public void ReleaseAllMonsters() => MonsterPool?.ReleaseAll();
}
