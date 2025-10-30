using UnityEngine;

public class AttackNode : NodeBase
{
    private readonly CharacterBase character;
    public AttackNode(CharacterBase character)
    {
        this.character = character;
        NodeName = "Attack";
    }
    public override NodeStatus Execute()
    {
        if (character.Target is null) return LogAndReturn(NodeStatus.Fail);
        float dist = Vector2.Distance(character.transform.position, character.Target.transform.position);
        if(dist > character.Data.AttackRange) return LogAndReturn(NodeStatus.Fail);
        if (character.Attack is null) return LogAndReturn(NodeStatus.Fail);
        character.Animator.SetTrigger("Attack");
        // TryStartAttack�� ��� �ߵ��� �õ��ϹǷ� ����� Success/Fail�� ��ȯ
        bool result = character.TryStartAttack();
        return LogAndReturn(result ? NodeStatus.Success : NodeStatus.Fail);
    }
}
