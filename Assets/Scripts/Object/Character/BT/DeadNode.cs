using UnityEngine;

public class DeadNode : NodeBase
{
    private readonly CharacterBase character;
    public DeadNode(CharacterBase character) => this.character = character;
    public override NodeStatus Execute()
    {
        if (character.Dead is null)return NodeStatus.Fail;
        character.Animator.SetTrigger("Death");
        character.Dead.Die();
        return NodeStatus.Running;
    }
}
