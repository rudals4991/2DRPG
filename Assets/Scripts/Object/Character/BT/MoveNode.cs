using UnityEngine;

public class MoveNode : NodeBase
{
    private readonly CharacterBase character;
    public MoveNode(CharacterBase character) => this.character = character;
    public override bool Execute()
    {
        //TODO: Move ���� ����
        return true;
    }
}
