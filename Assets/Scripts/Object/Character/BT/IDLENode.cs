using UnityEngine;

public class IDLENode : NodeBase
{
    private readonly CharacterBase character;
    public IDLENode(CharacterBase character)
    {
        this.character = character;
        NodeName = "IDLE";
    }

    public override NodeStatus Execute()
    {
        if (character.IDLE is null) return LogAndReturn(NodeStatus.Fail);

        character.IDLE.IDLE();
        return LogAndReturn(NodeStatus.Success); // 대기 상태는 지속, 마지막 노드이기 때문에 Success
    }
}
