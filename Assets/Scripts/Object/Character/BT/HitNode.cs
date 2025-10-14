using UnityEngine;

public class HitNode : NodeBase
{
    private readonly CharacterBase character;
    public HitNode(CharacterBase character) => this.character = character;
    public override NodeStatus Execute()
    {
        if (character.Damaged is null) return NodeStatus.Fail;

        character.Damaged.GetDamage();
        character.Status.SetHit(false); // 피격 플래그 해제
        return NodeStatus.Success;
    }
}
