using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //Status�� �и�
    //CharacterBase ��ü�� ����/�÷��̾�� ĳ���� ��� ��밡���ϵ��� ����


    [SerializeField] protected CharacterData data;

    #region ����
    public CharacterType CharacterType { get; private set; } // ����
    public string Name { get; private set; }                 // �̸�
    public int Level { get; private set; }                   // ����
    public int Cost { get; private set; }                    // ����
    #endregion

    #region ����
    public int MaxHp { get; private set; }                   // �ִ�ü��
    public int NowHp { get; private set; }                   // ����ü��
    public int MaxMp { get; private set; }                   // �ִ븶��
    public int NowMp { get; private set; }                   // ���縶��
    public float MoveSpeed { get; private set; }             // �̵��ӵ�
    public float AttackSpeed { get; private set; }           // ���ݼӵ�
    public float AttackDamage { get; private set; }          // �⺻ ���� ������
    public float CriticalRate { get; private set; }          // ġ��Ÿ Ȯ��
    public float Range { get; private set; }                 // ���� ��Ÿ�
    #endregion

    #region ���
    public virtual void Initialize()                         // �ʱ�ȭ
    {
        CharacterType = data.CharacterType;
        Name = data.Name;
        Level = 1;
        Cost = data.Cost;

        MaxHp = data.MaxHp;
        MaxMp = data.MaxMp;
        MoveSpeed = data.MoveSpeed;
        AttackSpeed = data.AttackSpeed;
        AttackDamage = data.AttackDamage;
        CriticalRate = data.CriticalRate;
        Range = data.Range;

        NowHp = MaxHp;
        NowMp = 0;
    }
    public virtual void IncreaseLevel()                      // ���� ����
    {
        Level++;
        //TODO: ���� �� ü��, ���ݷ� ���� ���� ����
    }
    public virtual void ChangeHp(int value)                  // ü�� ����
    {
        if (NowHp + value > MaxHp) NowHp = MaxHp;
        else if (NowHp + value < 0) NowHp = 0;
        else NowHp += value;
    }
    public virtual void ChangeMp(int value)                  // ���� ����
    {
        if (NowMp + value > MaxMp) NowMp = MaxMp;
        else if (NowMp + value < 0) NowMp = 0;
        else NowMp += value;
    }
    #endregion

    #region ����(BT)
    public bool IsAlive => NowHp > 0;
    public bool IsHit { get; set; } = false;           // TODO: TakeDamage�� true, ���� �ð� �� false
    public int SkillMana { get; set; } = 50;           // TODO: SkillData.ReqMana ����
    public bool CanUseSkill => NowMp >= SkillMana;     
    public bool CanAttack { get; set; } = false;       // TODO: Ÿ�� �Ÿ� üũ
    public bool IsInStage { get; set; } = false;       // TODO: StageManager���� ����
    #endregion
}
