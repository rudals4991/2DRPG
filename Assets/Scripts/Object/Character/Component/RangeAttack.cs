using System;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    RangeObjectManager manager;
    CharacterBase owner;
    CharacterBase target;
    Vector2 dir;
    int damage;
    [SerializeField] float moveSpeed;
    [SerializeField] float lifeTime;
    float timer;
    public bool IsActive { get; private set; }
    public void Initialize(CharacterBase owner, CharacterBase target, Vector2 dir, int damage)
    { 
        this.owner = owner;
        this.target = target;
        this.dir = dir;
        this.damage = damage;
        timer = lifeTime;
        IsActive = true;
        if(manager is null) manager = DIContainer.Resolve<RangeObjectManager>();
        manager.Register(this);
    }
    public void Tick(float dt)
    {
        if (!IsActive) return;
        if (target is null) return;
        transform.Translate(dir * moveSpeed * dt,Space.World);
        timer -= dt;
        if (timer <= 0)
        {
            ReleaseToPool();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable target))
        {
            if ((object)target == owner) return;
            if (CheckTeam(collision)) return;
            target.ApplyDamage(damage);
            ReleaseToPool();
        }
    }
    public bool CheckTeam(Collider2D col)
    {
        bool ownerPlayer = owner is PlayerBase;
        bool ownerMonster = owner is MonsterBase;
        bool hitPlayer = col.GetComponent<PlayerBase>() != null;
        bool hitMonster = col.GetComponent<MonsterBase>() != null;
        if((ownerPlayer && hitPlayer) || (ownerMonster && hitMonster)) return true;
        return false;
    }
    public void ReleaseToPool()
    { 
        if(manager is null) manager = DIContainer.Resolve<RangeObjectManager>();
        manager.Release(this);
    }
    public void Reset()
    {
        IsActive = false;
        owner = null;
        target = null;
    }
}
