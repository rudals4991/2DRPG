using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    public int MaxMp { get; private set; }
    public int NowMp { get; private set; }
    public int Level { get; private set; }
    public int Cost { get; private set; }
    public float CriticalRate { get; private set; }
    public bool IsHit { get; private set; }
    public int SkillMp { get; private set; }
    public bool CanUseSkill => NowMp >= SkillMp;
    public PlayerStatus(CharacterData data, int maxMp, int cost, float criticalRate, int skillMp) : base(data)
    {
        MaxMp = maxMp;
        NowMp = MaxMp;
        Level = 1;
        Cost = cost;
        CriticalRate = criticalRate;
        SkillMp = skillMp;
    }
}
