using UnityEngine;

public class DeadNode : NodeBase
{
    private readonly CharacterBase character;
    public DeadNode(CharacterBase character) => this.character = character;
    public override bool Execute()
    {
        //TODO: Dead ���� ����
        return true;
    }
}
