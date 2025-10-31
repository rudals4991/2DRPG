using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] SkillData skillData;
    PlayerBase player;
    PoolManager poolManager;
    private void Start()
    {
        player = GetComponent<PlayerBase>();
    }
    public bool UseSkill()
    {
        Debug.Log("Skill 사용");
        switch (skillData.SkillType)
        {
            case SkillType.AttackMelee: return UseAttackMeleeSkill(); 
            case SkillType.AttackRange: return UseAttackRangeSkill();
            case SkillType.Heal: return UseHealSkill();
            case SkillType.Buff: return UseBuffSkill();
            case SkillType.CC: return UseCCSkill();
            default: return false;
        }
    }
    public bool UseAttackMeleeSkill()
    {
        Effect(Color.gold);
        Collider2D[] targets = Physics2D.OverlapCircleAll(player.transform.position, player.Data.AttackRange);
        foreach (var t in targets)
        {
            if (t.TryGetComponent(out IDamagable dmg) && t.gameObject != player.gameObject)
            {
                dmg.ApplyDamage(skillData.Damage);
            }
        }
        return true;
    }
    public bool UseAttackRangeSkill()
    {
        Effect(Color.brown);
        if (player.Target is null) return false;
        if (poolManager is null) poolManager = DIContainer.Resolve<PoolManager>();
        GameObject attacker = poolManager.ArrowPool.Get(player.transform.position, player.transform.rotation);
        Vector2 dir = (player.Target.transform.position - player.transform.position).normalized;
        if (attacker.TryGetComponent(out RangeAttack range))
        {
            range.Initialize(player, player.Target, dir, skillData.Damage);
        }
        return true;
    }
    public bool UseHealSkill()
    {
        Effect(Color.blue);
        Collider2D[] allies = Physics2D.OverlapCircleAll(player.transform.position, player.Data.AttackRange);
        foreach (var t in allies)
        {
            if (t.TryGetComponent(out PlayerBase ally))
            {
                ally.Status.Heal(skillData.Damage);
            }
        }
        return true;
    }
    public bool UseBuffSkill()
    {
        Effect(Color.yellow);
        //TODO: 버프 스킬 사용
        return true;
    }
    public bool UseCCSkill()
    {
        Effect(Color.red);
        //TODO: CC스킬 사용
        return true;
    }
    public void Effect(Color color)
    {
        Vector3 originScale = player.transform.localScale;

        player.transform.localScale = originScale * 1.3f;
        StartCoroutine(ResetEffect(originScale));
    }
    IEnumerator ResetEffect(Vector3 originScale)
    {
        yield return new WaitForSeconds(0.4f);
        player.transform.localScale = originScale;
    }
}
