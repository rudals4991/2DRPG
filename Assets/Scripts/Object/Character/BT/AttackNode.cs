using UnityEngine;

public class AttackNode : NodeBase
{
    private readonly CharacterBase character;
    private readonly Transform target;
    public AttackNode(CharacterBase character, Transform target)
    {
        this.character = character;
        this.target = target;
    }
    public override NodeStatus Execute()
    {
        if (target is null) return NodeStatus.Fail;
        float dist = Vector2.Distance(character.transform.position, target.position);
        if(dist > character.Attack.attackRange) return NodeStatus.Fail;
        if (character.Attack is null) return NodeStatus.Fail;

        // TryStartAttack�� ��� �ߵ��� �õ��ϹǷ� ����� Success/Fail�� ��ȯ
        return character.TryStartAttack() ? NodeStatus.Success : NodeStatus.Fail;
    }
}
