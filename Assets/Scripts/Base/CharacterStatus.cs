using UnityEngine;

[System.Serializable]
public class CharacterStatus
{
    public bool IsAlive { get; private set; }
    public bool CanAttack { get; private set; }
    public bool IsInStage { get; private set; }
    private int maxHp;
    private int nowHp;
    public CharacterStatus(CharacterData data)
    {
        maxHp = data.MaxHp;
        nowHp = maxHp;
        IsAlive = true;
        CanAttack = false;
        IsInStage = false;
    }
}
