using System;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    [Header("Player Params")]
    [SerializeField] int maxMp = 100;
    [SerializeField, Range(0f, 1f)] float criticalRate = 0.1f;
    [SerializeField] int skillMp = 0;

    [Header("Skill CoolTime")]
    [SerializeField] float skillCoolTime = 5f;
    float _skillCoolTime;
    int level;
    PoolManager pool;
    CharacterManager characterManager;
    public static event Action<PlayerBase> OnPlayerDead;
    public PlayerSkill PlayerSkill { get; private set; }
    public PlayerStatus PlayerStatus => status as PlayerStatus;
    public bool IsSkillOffCooldown => _skillCoolTime <= 0f;
    public bool CanUseSkill        // MP + 쿨타임 모두 충족
    {
        get
        {
            var ps = status as PlayerStatus;
            return ps != null && ps.CanUseSkill && IsSkillOffCooldown;
        }
    }
    public override void Initialize(CharacterData data)
    {
        this.data = data;

        level = SaveManager.Instance.GetCharacterLevel(data.ID);

        Animator = GetComponentInChildren<Animator>();
        Move = GetComponent<CharacterMove>();
        Attack = GetComponent<CharacterAttack>();
        Damaged = GetComponent<CharacterDamaged>();
        Dead = GetComponent<CharacterDead>();
        IDLE = GetComponent<CharacterIDLE>();

        status = new PlayerStatus(data, level, maxMp, data.Cost, criticalRate, skillMp);

        PlayerSkill = GetComponent<PlayerSkill>();
        pool = DIContainer.Resolve<PoolManager>();
        characterManager = DIContainer.Resolve<CharacterManager>();
        characterManager.Register(this);

        _skillCoolTime = skillCoolTime;

        characterBT = new CharacterBT(this);
    }
    public override void Tick(float dt)
    {
        base.Tick(dt);
        if (_skillCoolTime > 0) _skillCoolTime -= dt;
    }
    public void OnAttackSuccess()
    {
        var ps = status as PlayerStatus;
        if (ps == null) return;

        ps.GetMp(10);
    }
    public bool TryCastSkill()
    {
        if (!Status.IsAlive || !IsSkillOffCooldown) return false;

        var ps = status as PlayerStatus;
        if (ps == null || !ps.CanUseSkill) return false;
        if (PlayerSkill == null) return false;
        Animator.SetTrigger("Skill");
        // 실제 스킬 수행
        if (!PlayerSkill.UseSkill()) return false;

        // MP 소모
        if (!ps.TryUseSkill()) return false;

        // 쿨다운 시작
        _skillCoolTime = skillCoolTime;
        return true;
    }
    protected override void OnDamaged(int dmg)
    {
        status.TakeDamage(dmg);
        Debug.Log($"{name}의 체력 {status.NowHp} 남음");
        if (status.NowHp <= 0)
        {
            OnPlayerDead?.Invoke(this);
            foreach (var c in characterManager.AllCharacters)
            {
                if (c.Target == this) c.SetTarget(null);
            }

            if (TryGetComponent(out Collider2D col)) col.enabled = false;

            if (pool is null) Destroy(gameObject);
            else
            {
                PlayerStatus.Reset(data, level, maxMp, data.Cost, criticalRate, skillMp);
                pool.PlayerPool.Release(data, gameObject);
            }
            cm.Unregister(this);
        } 
    }
}
