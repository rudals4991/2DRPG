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
        return LogAndReturn(NodeStatus.Success); // ��� ���´� ����, ������ ����̱� ������ Success
    }
}
