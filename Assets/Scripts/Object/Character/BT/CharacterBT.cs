using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterBT
{
    private NodeBase root;
    public CharacterBT(CharacterBase character)
    { 
        var idle = new IDLENode(character);
        var move = new MoveNode(character);
        var attack = new AttackNode(character);
        var skill = new SkillNode(character);
        var hit = new HitNode(character);
        var dead = new DeadNode(character);

        var isAlive = new ConditionNode(() => character.IsAlive);
        var isDead = new ConditionNode(() => !character.IsAlive);
        var isHit = new ConditionNode(() => character.IsHit);
        var canUseSkill = new ConditionNode(() => character.CanUseSkill);
        var canAttack = new ConditionNode(() => character.CanAttack);
        var isInStage = new ConditionNode(() => character.IsInStage);

        var deadSeq = new Sequence(new List<NodeBase> { isDead, dead });
        var hitSeq = new Sequence(new List<NodeBase> { isHit, hit });
        var skillSeq = new Sequence(new List<NodeBase> { canUseSkill, skill });
        var attackSeq = new Sequence(new List<NodeBase> { canAttack, attack });
        var moveSeq = new Sequence(new List<NodeBase> { isInStage, move });

        root = new Selector(new List<NodeBase>
        {
            deadSeq,
            hitSeq,
            skillSeq,
            attackSeq,
            moveSeq,
            idle
        });
    }
    public void Update()
    {
        root.Execute();
    }
}
