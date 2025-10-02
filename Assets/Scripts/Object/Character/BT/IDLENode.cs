using UnityEngine;

public class IDLENode : NodeBase
{
    private readonly CharacterBase character;
    public IDLENode(CharacterBase character) => this.character = character;

    public override bool Execute()
    {
        character.IDLE.IDLE();
        return true;
    }
}
