using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public int ID;                      // 고유 번호
    public CharacterType CharacterType; // 역할
    public AttackType AttackType;       // 공격방식
    public string Name;                 // 이름
    public int TypeIndex;               // 역할 내 순서
    public Sprite myImage;              // 대표 이미지

    [Header("스탯")]
    public int BaseMaxHp;               // 최대체력
    public float MoveSpeed;             // 이동속도
    public float AttackSpeed;           // 공격속도
    public int BaseAttackDamage;        // 기본 공격 데미지
    public int AttackRange;             // 공격 사거리

    [Header("해금")]
    public int Cost;                    // 해금 비용

    [Header("성장")]
    public int MaxLevel;                // 최대레벨
    public int LevelUpCost;             // 레벨업 비용
    public int AttackAmount;            // 공격력 증가량
    public int HpAmount;                // 체력 증가량
}
