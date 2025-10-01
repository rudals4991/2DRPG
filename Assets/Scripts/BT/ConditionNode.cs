using System;
using UnityEngine;

public class ConditionNode : NodeBase
{
    private readonly Func<bool> condition;
    public ConditionNode(Func<bool> condition) => this.condition = condition;
    public override bool Execute() => condition();
}
