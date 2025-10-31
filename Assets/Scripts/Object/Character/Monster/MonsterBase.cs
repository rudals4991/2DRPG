using UnityEngine;

public class MonsterBase : CharacterBase
{
    [Header("���� ���� �ε���")]
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
        Debug.Log($"{name}�� ü�� {status.NowHp} ����");
        if (status.NowHp <= 0)
        {
            var cm = DIContainer.Resolve<CharacterManager>();
            foreach (var c in cm.AllCharacters)
            {
                if (c.Target == this) c.SetTarget(null);
            }

            if (TryGetComponent(out Collider2D col)) col.enabled = false;

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
