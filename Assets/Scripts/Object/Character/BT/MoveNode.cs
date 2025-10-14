using UnityEngine;

public class MoveNode : NodeBase
{
    private readonly CharacterBase character;
    public MoveNode(CharacterBase character) => this.character = character;
    public override NodeStatus Execute()
    {
        if (character.Move is null) return NodeStatus.Fail;
        bool isMoving = character.Move.Move();
        return isMoving ? NodeStatus.Running : NodeStatus.Success;
    }
}
