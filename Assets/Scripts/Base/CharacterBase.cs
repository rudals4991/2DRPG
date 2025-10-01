using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterData data;

    //CHaracter Info
    public CharacterType CharacterType { get; private set; } // 역할
    public string Name { get; private set; }                 // 이름
    public int Level { get; private set; }                   // 레벨
    public int Cost { get; private set; }                    // 가격
    public bool IsAlive => NowHp > 0;                        // 생존여부

    //Character State
    public int MaxHp { get; private set; }                   // 최대체력
    public int NowHp { get; private set; }                   // 현재체력
    public int MaxMp { get; private set; }                   // 최대마나
    public int NowMp { get; private set; }                   // 현재마나
    public float MoveSpeed { get; private set; }             // 이동속도
    public float AttackSpeed { get; private set; }           // 공격속도
    public float AttackDamage { get; private set; }          // 기본 공격 데미지
    public float CriticalRate { get; private set; }          // 치명타 확률
    public float Range { get; private set; }                 // 공격 사거리

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
