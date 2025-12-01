using System;
using UnityEngine;

public class MonsterBase : CharacterBase
{
    PoolManager pool;

    public static event Action<MonsterBase> OnMonsterDie;
    protected override CharacterStatus CreateStatus(CharacterData data, int level)
    {
        return new CharacterStatus(data, 1);
    }
    public override void Initialize(CharacterData data)
    {
        base.Initialize(data);
        pool = DIContainer.Resolve<PoolManager>();
    }
    protected override void OnDamaged(int dmg)
    {
        status.TakeDamage(dmg);
        Debug.Log($"{name}의 체력 {status.NowHp} 남음");
        if (status.NowHp <= 0)
        {
            OnMonsterDie?.Invoke(this);

            foreach (var c in cm.AllCharacters)
            {
                if (c.Target == this) c.SetTarget(null);
            }

            if (TryGetComponent(out Collider2D col)) col.enabled = false;

            if (pool is null) Destroy(gameObject);
            else
            {
                status.Reset(data,1);
                pool.MonsterPool.Release(data, gameObject);
            }
            cm.Unregister(this);
        }
    }
}
