using UnityEngine;

public class IDLENode : NodeBase
{
    private readonly CharacterBase character;
    public IDLENode(CharacterBase character) => this.character = character;

    public override NodeStatus Execute()
    {
        if (character.IDLE is null) return NodeStatus.Fail;

        character.IDLE.IDLE();
        return NodeStatus.Running; // 대기 상태는 지속
    }
}
