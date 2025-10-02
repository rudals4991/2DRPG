using UnityEngine;

public class AttackNode : NodeBase
{
    private readonly CharacterBase character;
    public AttackNode(CharacterBase character) => this.character = character;
    public override bool Execute()
    {
        //TODO: Attack 로직 구현
        return true;
    }
}
