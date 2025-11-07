using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour, IManagerBase
{
    List<ObjectPoolBase> poolList = new();
    PlayerPool playerPool;
    MonsterPool monsterPool;
    ArrowPool arrowPool;
    HpbarPool hpbarPool;
    public int Priority => 1;
    public PlayerPool PlayerPool => playerPool;
    public MonsterPool MonsterPool => monsterPool;
    public ArrowPool ArrowPool => arrowPool;
    public HpbarPool HpbarPool => hpbarPool;

    public void Exit()
    {
        ReleaseAll();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        ConnectPool();
    }
    public void ConnectPool()
    {
        playerPool = DIContainer.Resolve<PlayerPool>();
        monsterPool = DIContainer.Resolve<MonsterPool>();
        arrowPool = DIContainer.Resolve<ArrowPool>();
        hpbarPool = DIContainer.Resolve<HpbarPool>();

        poolList.Add(playerPool);
        poolList.Add(monsterPool);
        poolList.Add(arrowPool);
        poolList.Add(hpbarPool);

        playerPool.Initialize();
        monsterPool.Initialize();
        arrowPool.Initialize();
        hpbarPool.Initialize();
    }
    public GameObject SpawnPlayer(CharacterData data, Vector3 pos, Quaternion rot)
    {
        if (PlayerPool == null) return null;
        return PlayerPool.Get(data, pos, rot);
    }

    public GameObject SpawnMonster(CharacterData data, Vector3 pos, Quaternion rot)
    {
        if (MonsterPool == null) return null;
        return MonsterPool.Get(data, pos, rot);
    }
    public void ReleaseAll()
    {
        foreach (ObjectPoolBase pool in poolList)
        {
            pool.ReleaseAll();
        }
    }
    public void ReleaseAllPlayers() => PlayerPool?.ReleaseAll();
    public void ReleaseAllMonsters() => MonsterPool?.ReleaseAll();
}
