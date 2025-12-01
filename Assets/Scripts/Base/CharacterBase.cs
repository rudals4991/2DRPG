using UnityEngine;

public abstract class CharacterBase : MonoBehaviour,IDamagable
{
    [SerializeField] protected CharacterData data;
    public CharacterMove Move { get; protected set; }
    public CharacterAttack Attack { get; protected set; }
    public CharacterDamaged Damaged { get; protected set; }
    public CharacterDead Dead { get; protected set; }
    public CharacterIDLE IDLE { get; protected set; }
    public Animator Animator { get; protected set; }
    public CharacterBase Target { get; protected set; }

    protected CharacterStatus status;
    protected CharacterBT characterBT;
    protected CharacterManager cm;
    public CharacterStatus Status => status;
    float attackCoolTime;
    float attackInterval;

    public CharacterData Data => data;

    protected virtual CharacterStatus CreateStatus(CharacterData data, int level)
    { 
        return new CharacterStatus(data, level);
    }
    public virtual void Initialize()                         // 초기화
    {
        Animator = GetComponentInChildren<Animator>();
        Move = GetComponent<CharacterMove>();
        Attack = GetComponent<CharacterAttack>();
        Damaged = GetComponent<CharacterDamaged>();
        Dead = GetComponent<CharacterDead>();
        IDLE = GetComponent<CharacterIDLE>();
        int level = SaveManager.Instance.GetCharacterLevel(data.ID);
        status = CreateStatus(data, level);
        status.SetInStage(true);
        characterBT = new CharacterBT(this);
        attackInterval = data.AttackSpeed <= 0 ? 1f : 1f / data.AttackSpeed;
        attackCoolTime = 0f;
        Damaged.OnDamaged -= OnDamaged;
        Damaged.OnDamaged += OnDamaged;
        cm = DIContainer.Resolve<CharacterManager>();
        cm.Register(this);
        Debug.Log($"{data.Name} 초기화 완료 → Target: {Target}");
    }
    public virtual void Initialize(CharacterData data)
    {
        this.data = data;
        Initialize();       
    }
    public virtual void Tick(float dt)
    {
        if (attackCoolTime > 0f) attackCoolTime -= dt;
        status.SetCanAttack(attackCoolTime <= 0);
        //TODO: 피격 무적 연동
        Damaged?.Tick(dt);
        if (status.IsHit && (Damaged?.IsInvincibleOver() ?? true)) status.SetHit(false);
        characterBT.Update();
    }
    public virtual bool TryStartAttack()
    {
        if (!status.IsAlive || !status.CanAttack) return false;
        if (Attack is null) return false; //TODO: 실제 공격 로직 추가
        attackCoolTime = attackInterval;
        status.SetCanAttack(false);
        Attack.Attack();
        if (this is PlayerBase player)
        {
            player.OnAttackSuccess();
        }

        return true;
    }
    protected virtual void OnDamaged(int dmg)
    {
        status.TakeDamage(dmg);
        Debug.Log($"{name}의 체력 {status.NowHp} 남음");
        if (status.NowHp <= 0) Destroy(gameObject);
    }
    public void ApplyDamage(int damage)
    { 
        //인터페이스 호출용
        Damaged.ApplyDamage(damage);
    }
    public void SetTarget(CharacterBase target)
    { 
        Target = target;
    }
}
