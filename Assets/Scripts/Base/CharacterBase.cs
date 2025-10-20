using UnityEngine;

public abstract class CharacterBase : MonoBehaviour,IDamagable
{
    [SerializeField] protected CharacterData data;
    public CharacterMove Move { get; private set; }
    public CharacterAttack Attack { get; private set; }
    public CharacterDamaged Damaged { get; private set; }
    public CharacterDead Dead { get; private set; }
    public CharacterIDLE IDLE { get; private set; }

    protected CharacterStatus status;
    public CharacterStatus Status => status;

    [Header("Attack CoolTime")]
    [SerializeField] float attackCoolTime = 0.5f;
    float _attackCoolTime;

    public virtual void Initialize()                         // �ʱ�ȭ
    {
        Move = GetComponent<CharacterMove>();
        Attack = GetComponent<CharacterAttack>();
        Damaged = GetComponent<CharacterDamaged>();
        Dead = GetComponent<CharacterDead>();
        IDLE = GetComponent<CharacterIDLE>();
        status = new CharacterStatus(data);
        _attackCoolTime = 0f;
        //TODO: ������ �̺�Ʈ ����
        Damaged.OnDamaged -= OnDamaged;
        Damaged.OnDamaged += OnDamaged;
    }
    public virtual void Tick(float dt)
    {
        if (_attackCoolTime > 0f) _attackCoolTime -= dt;
        status.SetCanAttack(_attackCoolTime <= 0);
        //TODO: �ǰ� ���� ����
        Damaged?.Tick(dt);
        if (status.IsHit && (Damaged?.IsInvincibleOver() ?? true)) status.SetHit(false);
    }
    public bool TryStartAttack()
    {
        if (!status.IsAlive || !status.CanAttack) return false;
        if (Attack is null) return false; //TODO: ���� ���� ���� �߰�
        _attackCoolTime = attackCoolTime;
        status.SetCanAttack(false);
        Attack.Attack();
        return true;
    }
    private void OnDamaged(int dmg)
    {
        status.TakeDamage(dmg);
    }
    public void ApplyDamage(int damage)
    { 
        //�������̽� ȣ���
        Damaged.ApplyDamage(damage);
    }
    private void OnDead()
    {
        status.Kill();
    }
}
