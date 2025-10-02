using UnityEngine;

public class SkillNode : NodeBase
{
    private readonly PlayerBase player;
    public SkillNode(PlayerBase player) => this.player = player;
    public override bool Execute()
    {
        player.PlayerSkill.UseSkill();
        return true;
    }
}
