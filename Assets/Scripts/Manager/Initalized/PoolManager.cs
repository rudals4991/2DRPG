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
    //[SerializeField] ObjectPoolBase[] allPool;
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
        poolList.Add(playerPool);
        if (playerPool is null) Debug.Log("PlayerPool");
        playerPool.Initialize();

        monsterPool = DIContainer.Resolve<MonsterPool>();
        poolList.Add(monsterPool);
        if (playerPool is null) Debug.Log("MonsterPool");
        monsterPool.Initialize();

        arrowPool = DIContainer.Resolve<ArrowPool>();
        poolList.Add(arrowPool);
        if (playerPool is null) Debug.Log("ArrowPool");
        arrowPool.Initialize();

        hpbarPool = DIContainer.Resolve<HpbarPool>();
        poolList.Add(hpbarPool);
        if (playerPool is null) Debug.Log("HpbarPool");
        hpbarPool.Initialize();
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
        foreach (ObjectPoolBase pool in poolList)
        {
            pool.ReleaseAll();
        }
    }
    public void ReleaseAllPlayers() => PlayerPool?.ReleaseAll();
    public void ReleaseAllMonsters() => MonsterPool?.ReleaseAll();
}
