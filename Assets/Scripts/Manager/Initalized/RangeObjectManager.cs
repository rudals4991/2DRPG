using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeObjectManager : MonoBehaviour, IManagerBase
{
    PoolManager poolManager;
    readonly List<RangeAttack> rangeList = new();
    public int Priority => 12;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        poolManager = DIContainer.Resolve<PoolManager>();
    }
    public void Register(RangeAttack range)
    {
        if (!rangeList.Contains(range))
            rangeList.Add(range);
    }
    public void Release(RangeAttack range)
    {
        range.Reset();
        rangeList.Remove(range);
        poolManager.ArrowPool.Release(range.gameObject);
    }
    public void Tick(float dt)
    {
        for (int i = rangeList.Count - 1; i >= 0; i--)
        {
            rangeList[i].Tick(dt);
        }
    }
}
