using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //Status로 분리
    //CharacterBase 자체는 몬스터/플레이어블 캐릭터 모두 사용가능하도록 설계


    [SerializeField] protected CharacterData data;

    #region 정보
    public CharacterType CharacterType { get; private set; } // 역할
    public string Name { get; private set; }                 // 이름
    public int Level { get; private set; }                   // 레벨
    public int Cost { get; private set; }                    // 가격
    #endregion

    #region 스탯
    public int MaxHp { get; private set; }                   // 최대체력
    public int NowHp { get; private set; }                   // 현재체력
    public int MaxMp { get; private set; }                   // 최대마나
    public int NowMp { get; private set; }                   // 현재마나
    public float MoveSpeed { get; private set; }             // 이동속도
    public float AttackSpeed { get; private set; }           // 공격속도
    public float AttackDamage { get; private set; }          // 기본 공격 데미지
    public float CriticalRate { get; private set; }          // 치명타 확률
    public float Range { get; private set; }                 // 공격 사거리
    #endregion

    #region 기능
    public virtual void Initialize()                         // 초기화
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
    public virtual void IncreaseLevel()                      // 레벨 증가
    {
        Level++;
        //TODO: 레벨 별 체력, 공격력 증가 로직 구현
    }
    public virtual void ChangeHp(int value)                  // 체력 변경
    {
        if (NowHp + value > MaxHp) NowHp = MaxHp;
        else if (NowHp + value < 0) NowHp = 0;
        else NowHp += value;
    }
    public virtual void ChangeMp(int value)                  // 마나 변경
    {
        if (NowMp + value > MaxMp) NowMp = MaxMp;
        else if (NowMp + value < 0) NowMp = 0;
        else NowMp += value;
    }
    #endregion

    #region 상태(BT)
    public bool IsAlive => NowHp > 0;
    public bool IsHit { get; set; } = false;           // TODO: TakeDamage시 true, 일정 시간 후 false
    public int SkillMana { get; set; } = 50;           // TODO: SkillData.ReqMana 참조
    public bool CanUseSkill => NowMp >= SkillMana;     
    public bool CanAttack { get; set; } = false;       // TODO: 타겟 거리 체크
    public bool IsInStage { get; set; } = false;       // TODO: StageManager에서 세팅
    #endregion
}
