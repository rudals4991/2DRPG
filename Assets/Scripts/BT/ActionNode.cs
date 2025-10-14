using System;
using UnityEngine;

public class ActionNode : NodeBase
{
    private readonly Func<NodeStatus> action;
    public ActionNode(Func<NodeStatus> action) => this.action = action;
    public override NodeStatus Execute() => action();
}
