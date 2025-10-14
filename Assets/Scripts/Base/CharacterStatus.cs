using System;
using UnityEngine;

[System.Serializable]
public class CharacterStatus
{
    public bool IsAlive { get; protected set; }
    public bool CanAttack { get; protected set; }
    public bool IsInStage { get; protected set; }
    public bool IsHit { get; protected set; }
    public int MaxHP => maxHp;
    public int NowHp => nowHp;
    private int maxHp;
    private int nowHp;
    public CharacterStatus(CharacterData data)
    {
        maxHp = data.MaxHp;
        nowHp = maxHp;
        IsAlive = true;
        CanAttack = false;
        IsInStage = true;
        IsHit = false;
    }
    public void SetInStage(bool value) => IsInStage = value;
    public void SetHit(bool value) => IsHit = value;
    public void SetCanAttack(bool value) => CanAttack = value;
    public int TakeDamage(int damage)
    {
        if (!IsAlive) return 0;
        int deal = Math.Max(0, Math.Min(damage, nowHp));
        nowHp = Math.Max(0, nowHp - deal);
        if (nowHp <= 0) IsAlive = false;
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
}
