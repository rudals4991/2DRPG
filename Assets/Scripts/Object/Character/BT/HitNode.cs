using UnityEngine;

public class HitNode : NodeBase
{
    private readonly CharacterBase character;
    public HitNode(CharacterBase character) => this.character = character;
    public override NodeStatus Execute()
    {
        if (character.Damaged is null) return NodeStatus.Fail;
        character.Damaged.GetDamaged();
        return NodeStatus.Success;
    }
}
