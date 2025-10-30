using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    CharacterBase character;
    PoolManager poolManager;
    BuffBase buff;
    public void Attack()
    {
        if (character is null) character = GetComponent<CharacterBase>();
        switch (character.Data.AttackType)
        {
            case AttackType.Melee: MeleeAttack(); break;
            case AttackType.Range: RangeAttacks(); break;
            case AttackType.Heal: Heal(); break;
            case AttackType.Buff: Buff(); break;
        }
    }

    public void MeleeAttack()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, character.Data.AttackRange);
        foreach (var target in targets)
        {
            if (target.gameObject == gameObject) continue;
            if (target.TryGetComponent(out IDamagable damage)) damage.ApplyDamage(character.Data.AttackDamage);
        }
    }
    public void RangeAttacks()
    {
        if (character.Target is null) return;
        if(poolManager is null) poolManager = DIContainer.Resolve<PoolManager>();
        GameObject attacker = poolManager.ArrowPool.Get(character.transform.position,character.transform.rotation);
        Vector2 dir = (character.Target.transform.position - character.transform.position).normalized;
        if (attacker.TryGetComponent(out RangeAttack range))
        {
            range.Initialize(character, character.Target, dir, character.Data.AttackDamage);
        }
        
        
    }
    public void ControlRangeAttack(RangeAttack range)
    {
        if (poolManager is null) poolManager = DIContainer.Resolve<PoolManager>();
        poolManager.ArrowPool.Release(range.gameObject);
    }

    public void Heal()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, character.Data.AttackRange);
        foreach (var target in targets)
        {
            if (target.TryGetComponent(out PlayerBase player)) player.Status.Heal(character.Data.AttackDamage);
        }
    }
    public void Buff()
    {
        if(buff is null) buff = GetComponent<BuffBase>();
        buff.Buff(character);
    }
}
