using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public CharacterType CharacterType; // 역할
    public AttackType AttackType;       // 공격방식
    public string Name;                 // 이름

    [Header("스탯")]
    public int MaxHp;                   // 최대체력
    public int NowHp;                   // 현재체력
    public float MoveSpeed;             // 이동속도
    public float AttackSpeed;           // 공격속도
    public int AttackDamage;            // 기본 공격 데미지
    public int AttackRange;             // 공격 사거리
    public int Cost;                    // 해금 비용
}
