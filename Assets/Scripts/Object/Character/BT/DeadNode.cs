using UnityEngine;

public class DeadNode : NodeBase
{
    private readonly CharacterBase character;
    public DeadNode(CharacterBase character)
    {
        this.character = character;
        NodeName = "Dead";
    }
    public override NodeStatus Execute()
    {
        if (character.Dead is null)return LogAndReturn(NodeStatus.Fail);
        character.Animator.SetTrigger("Death");
        character.Dead.Die();
        return LogAndReturn(NodeStatus.Running);
    }
}
