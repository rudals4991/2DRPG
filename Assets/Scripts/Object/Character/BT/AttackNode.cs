using UnityEngine;

public class AttackNode : NodeBase
{
    private readonly CharacterBase character;
    public AttackNode(CharacterBase character) => this.character = character;
    public override NodeStatus Execute()
    {
        if (character.Attack is null) return NodeStatus.Fail;

        // TryStartAttack은 즉시 발동만 시도하므로 결과로 Success/Fail만 반환
        return character.TryStartAttack() ? NodeStatus.Success : NodeStatus.Fail;
    }
}
