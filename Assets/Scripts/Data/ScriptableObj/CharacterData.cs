using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public CharacterType CharacterType; // 역할
    public string Name;                 // 이름

    [Header("스탯")]
    public int MaxHp;                   // 최대체력
    public int NowHp;                   // 현재체력
    public float MoveSpeed;             // 이동속도
    public float AttackSpeed;           // 공격속도
    public float AttackDamage;          // 기본 공격 데미지
    public float AttackRange;           // 공격 사거리
}
