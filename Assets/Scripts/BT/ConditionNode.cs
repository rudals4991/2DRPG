using System;
using UnityEngine;

public class ConditionNode : NodeBase
{
    private readonly Func<bool> condition;
    public ConditionNode(Func<bool> condition) => this.condition = condition;
    public override NodeStatus Execute()
    {
        return condition() ? NodeStatus.Success : NodeStatus.Fail;
    }
}
