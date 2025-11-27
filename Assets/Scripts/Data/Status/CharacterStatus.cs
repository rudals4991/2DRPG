using System;
using UnityEngine;

[System.Serializable]
public class CharacterStatus
{
    public bool IsAlive { get; protected set; }
    public bool CanAttack { get; protected set; }
    public bool IsInStage { get; protected set; }
    public bool IsHit { get; protected set; }
    public int AttackDamage { get; private set; }
    public int MaxHP => maxHp;
    public int NowHp => nowHp;
    public int Level => level;
    private int maxHp;
    private int nowHp;
    private int level;
    public CharacterStatus(CharacterData data, int level)
    {
        this.level = Mathf.Clamp(level, 1, data.MaxLevel);
        SetStats(data);
        Live();
    }
    public void SetInStage(bool value) => IsInStage = value;
    public void SetHit(bool value) => IsHit = value;
    public void SetCanAttack(bool value) => CanAttack = value;
    public int TakeDamage(int damage)
    {
        if (!IsAlive) return 0;
        int deal = Math.Max(0, Math.Min(damage, nowHp));
        nowHp = Math.Max(0, nowHp - deal);
        
        if (nowHp <= 0) Kill();
        IsHit = true;
        return deal;
    }
    public void Heal(int amount)
    {
        if (!IsAlive) return;
        nowHp = Math.Min(maxHp, nowHp + amount);
    }
    public void Kill()
    {
        nowHp = 0;
        IsAlive = false;
    }
    public void Live()
    {
        nowHp = maxHp;
        IsAlive = true;
        IsHit = false;
    }
    public void SetStats(CharacterData data)
    {
        maxHp = data.BaseMaxHp + data.HpAmount * (level - 1);
        nowHp = maxHp;
        AttackDamage = data.BaseAttackDamage + data.AttackAmount * (level - 1);
    }
    public virtual void Reset(CharacterData data,int level)
    {
        this.level = Mathf.Clamp(level, 1, data.MaxLevel);
        SetStats(data);
        Live();
    }
}
