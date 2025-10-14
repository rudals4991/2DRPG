using UnityEngine;

public class SkillNode : NodeBase
{
    private readonly CharacterBase c;
    public SkillNode(CharacterBase c) => this.c = c;
    public override NodeStatus Execute()
    {
        var player = c as PlayerBase;
        if (player is null) return NodeStatus.Fail;
        return player.TryCastSkill() ? NodeStatus.Success : NodeStatus.Fail;
    }
}
