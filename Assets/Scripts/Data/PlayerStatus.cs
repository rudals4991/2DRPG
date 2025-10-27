using System;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    public int MaxMp { get; private set; }
    public int NowMp { get; private set; }
    public int Level { get; private set; }
    public int Cost { get; private set; }
    public float CriticalRate { get; private set; }
    public int SkillMp { get; private set; }
    public bool CanUseSkill => IsAlive && NowMp >= SkillMp;
    public PlayerStatus(CharacterData data, int maxMp, int cost, float criticalRate, int skillMp) : base(data)
    {
        MaxMp = maxMp;
        NowMp = 0;
        Level = 1;
        Cost = cost;
        CriticalRate = criticalRate;
        SkillMp = skillMp;
    }
    public bool TryUseSkill()
    {
        if (!CanUseSkill) return false;
        NowMp -= SkillMp;
        return true;
    }
    public void GetMp(int amount)
    {
        if (!IsAlive) return;
        NowMp = Math.Min(MaxMp, NowMp + amount);
    }
    public void LevelUp()
    {
        Level++;
        //TODO: 레벨업 시 추가 능력치 상승
    }
}
