using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public CharacterType CharacterType; // 역할
    public string Name;                 // 이름
    public int Cost;                    // 가격

    [Header("스탯")]
    public int MaxHp;                   // 최대체력
    public int MaxMp;                   // 최대마나
    public float MoveSpeed;             // 이동속도
    public float AttackSpeed;           // 공격속도
    public float AttackDamage;          // 기본 공격 데미지
    public float CriticalRate;          // 치명타 확률
    public float Range;                 // 공격 사거리
}
