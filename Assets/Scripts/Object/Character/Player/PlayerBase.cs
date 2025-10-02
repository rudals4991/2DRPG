using UnityEngine;

public class PlayerBase : CharacterBase
{
    public PlayerSkill PlayerSkill { get; private set; }
    public override void Initialize()
    {
        base.Initialize();
        PlayerSkill = GetComponent<PlayerSkill>();
    }
}
