using UnityEngine;

public class MonsterBase : CharacterBase
{
    [Header("몬스터 고유 인덱스")]
    [SerializeField] int myTypeIndex;
    PoolManager pool;
    public override void Initialize()
    {
        base.Initialize();
        pool = DIContainer.Resolve<PoolManager>();
    }
    protected override void OnDamaged(int dmg)
    {
        status.TakeDamage(dmg);
        Debug.Log($"{name}의 체력 {status.NowHp} 남음");
        if (status.NowHp <= 0)
        {
            if (TryGetComponent(out Collider2D col))
                col.enabled = false;

            if (pool is null) Destroy(gameObject);
            else
            {
                status.Reset(data);
                pool.MonsterPool.Release(myTypeIndex, gameObject);
            }
            cm.Unregister(this);
        }
    }
}
