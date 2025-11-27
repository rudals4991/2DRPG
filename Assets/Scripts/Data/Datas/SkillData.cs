using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/SkillData")]
public class SkillData : ScriptableObject
{
    public CharacterType UserType;
    public SkillType SkillType;
    public string SkillName;
    public int RequireMana;
    public int Damage;
}
