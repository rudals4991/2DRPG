using System.Collections.Generic;
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

        var isDead = new ConditionNode(() => !character.Status.IsAlive);
        var isHit = new ConditionNode(() => character.Status.IsHit);
        var canAttack = new ConditionNode(() => character.Status.CanAttack);
        var isInStage = new ConditionNode(() => character.Status.IsInStage);

        var isTargetInRange = new ConditionNode(() =>
        {
            if (character.Target == null) return false;
            float dist = Vector2.Distance(character.transform.position, character.Target.transform.position);
            return dist <= character.Data.AttackRange;
        });

        var canUseSkill = new ConditionNode(() =>
        {
            var player = character as PlayerBase;
            return player != null && player.CanUseSkill;
        });

        var deadSeq = new Sequence(new List<NodeBase> { isDead, dead });
        var hitSeq = new Sequence(new List<NodeBase> { isHit, hit });
        var skillSeq = new Sequence(new List<NodeBase> { canUseSkill, skill });
        var attackSeq = new Sequence(new List<NodeBase> { isTargetInRange, canAttack, attack });
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
