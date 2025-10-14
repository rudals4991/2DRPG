using UnityEngine;

public class AttackNode : NodeBase
{
    private readonly CharacterBase character;
    public AttackNode(CharacterBase character) => this.character = character;
    public override NodeStatus Execute()
    {
        if (character.Attack is null) return NodeStatus.Fail;

        // TryStartAttack�� ��� �ߵ��� �õ��ϹǷ� ����� Success/Fail�� ��ȯ
        return character.TryStartAttack() ? NodeStatus.Success : NodeStatus.Fail;
    }
}
