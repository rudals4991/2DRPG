using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterData data;

    //CHaracter Info
    public CharacterType CharacterType { get; private set; } // ����
    public string Name { get; private set; }                 // �̸�
    public int Level { get; private set; }                   // ����
    public int Cost { get; private set; }                    // ����
    public bool IsAlive => NowHp > 0;                        // ��������

    //Character State
    public int MaxHp { get; private set; }                   // �ִ�ü��
    public int NowHp { get; private set; }                   // ����ü��
    public int MaxMp { get; private set; }                   // �ִ븶��
    public int NowMp { get; private set; }                   // ���縶��
    public float MoveSpeed { get; private set; }             // �̵��ӵ�
    public float AttackSpeed { get; private set; }           // ���ݼӵ�
    public float AttackDamage { get; private set; }          // �⺻ ���� ������
    public float CriticalRate { get; private set; }          // ġ��Ÿ Ȯ��
    public float Range { get; private set; }                 // ���� ��Ÿ�

    //Charater Func
    public virtual void Initialize()
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
    public virtual void IncreaseLevel()
    {
        Level++;
    }
}
