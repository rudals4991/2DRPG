using UnityEngine;

public class HitNode : NodeBase
{
    private readonly CharacterBase character;
    public HitNode(CharacterBase character)
    {
        this.character = character;
        NodeName = "Hit";
    }
    public override NodeStatus Execute()
    {
        if (character.Damaged is null) return LogAndReturn(NodeStatus.Fail);
        character.Animator.SetTrigger("Damaged");
        character.Damaged.GetDamaged();
        return LogAndReturn(NodeStatus.Success);
    }
}
