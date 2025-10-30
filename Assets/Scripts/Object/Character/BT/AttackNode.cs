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
        // TryStartAttack은 즉시 발동만 시도하므로 결과로 Success/Fail만 반환
        bool result = character.TryStartAttack();
        return LogAndReturn(result ? NodeStatus.Success : NodeStatus.Fail);
    }
}
