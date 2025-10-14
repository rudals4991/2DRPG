using UnityEngine;

public class PlayerBase : CharacterBase
{
    [Header("Player Params")]
    [SerializeField] int maxMp = 100;
    [SerializeField] int cost = 0;
    [SerializeField, Range(0f, 1f)] float criticalRate = 0.1f;
    [SerializeField] int skillMp = 20;

    [Header("Skill CoolTime")]
    [SerializeField] float skillCoolTime = 5f;
    float _skillCoolTime;
    public PlayerSkill PlayerSkill { get; private set; }

    public bool IsSkillOffCooldown => _skillCoolTime <= 0f;
    public bool CanUseSkill        // MP + ��Ÿ�� ��� ����
    {
        get
        {
            var ps = status as PlayerStatus;
            return ps != null && ps.CanUseSkill && IsSkillOffCooldown;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        status = new PlayerStatus(data, maxMp, cost, criticalRate, skillMp);
        PlayerSkill = GetComponent<PlayerSkill>();
        _skillCoolTime = 0f;
    }
    public override void Tick(float dt)
    {
        base.Tick(dt);
        if (_skillCoolTime > 0) _skillCoolTime -= dt;
    }
    public bool TryCastSkill()
    {
        if (!Status.IsAlive || !IsSkillOffCooldown) return false;

        var ps = status as PlayerStatus;
        if (ps == null || !ps.CanUseSkill) return false;
        if (PlayerSkill == null) return false;

        // ���� ��ų ����
        if (!PlayerSkill.UseSkill()) return false;

        // MP �Ҹ�
        if (!ps.TryUseSkill()) return false;

        // ��ٿ� ����
        _skillCoolTime = skillCoolTime;
        return true;
    }
}
