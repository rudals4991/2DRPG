using UnityEngine;

public class MoveNode : NodeBase
{
    private readonly CharacterBase character;
    public MoveNode(CharacterBase character)
    {
        this.character = character;
        NodeName = "Move";
    }
    public override NodeStatus Execute()
    {
        if (character.Move is null) return LogAndReturn( NodeStatus.Fail);
        if (character.Target is null)
        {
            character.Move.Stop();
            return LogAndReturn(NodeStatus.Fail);
        }
        float dist = Vector2.Distance(character.transform.position, character.Target.transform.position);
        if (dist <= character.Attack.attackRange)
        {
            character.Move.Stop();
            return LogAndReturn(NodeStatus.Success);
        }
        character.Animator.SetTrigger("Move");
        character.Move.Move(character.Target.transform.position);
        return LogAndReturn(NodeStatus.Running);
    }
}
