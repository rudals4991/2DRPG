using UnityEngine;

public class HitNode : NodeBase
{
    private readonly CharacterBase character;
    public HitNode(CharacterBase character) => this.character = character;
    public override bool Execute()
    {
        //TODO: Hit ���� ����
        return true;
    }
}
