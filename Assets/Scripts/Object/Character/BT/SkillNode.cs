using UnityEngine;

public class SkillNode : NodeBase
{
    private readonly CharacterBase character;
    public SkillNode(CharacterBase character) => this.character = character;
    public override bool Execute()
    {
        return true;
    }
}
