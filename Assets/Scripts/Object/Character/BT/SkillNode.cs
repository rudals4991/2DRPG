using UnityEngine;

public class SkillNode : NodeBase
{
    private readonly CharacterBase c;
    public SkillNode(CharacterBase c)
    {
        this.c = c;
        NodeName = "Skill";
    }
    public override NodeStatus Execute()
    {
        var player = c as PlayerBase;
        if (player is null) return LogAndReturn(NodeStatus.Fail);
        if (player.Target == null || !player.Target.Status.IsAlive) return LogAndReturn(NodeStatus.Fail);
        bool result = player.TryCastSkill();
        return LogAndReturn(result ? NodeStatus.Success : NodeStatus.Fail);
    }
}
