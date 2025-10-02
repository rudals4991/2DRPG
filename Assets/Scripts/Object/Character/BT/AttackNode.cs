using UnityEngine;

public class AttackNode : NodeBase
{
    private readonly CharacterBase character;
    public AttackNode(CharacterBase character) => this.character = character;
    public override bool Execute()
    {
        character.Attack.Attack();
        return true;
    }
}
