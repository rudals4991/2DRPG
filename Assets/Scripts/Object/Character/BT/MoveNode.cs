using UnityEngine;

public class MoveNode : NodeBase
{
    private readonly CharacterBase character;
    private readonly Transform target;
    public MoveNode(CharacterBase character, Transform target)
    {
        this.character = character;
        this.target = target;
    }
    public override NodeStatus Execute()
    {
        if (character.Move is null) return NodeStatus.Fail;
        if (target is null)
        {
            character.Move.Stop();
            return NodeStatus.Fail;
        }
        float dist = Vector2.Distance(character.transform.position, target.position);
        if (dist <= character.Attack.attackRange)
        {
            character.Move.Stop();
            return NodeStatus.Success;
        }
        character.Animator.SetTrigger("Move");
        character.Move.Move(target.position);
        return NodeStatus.Running;
    }
}
