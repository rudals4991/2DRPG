using System;
using UnityEngine;

public class ActionNode : NodeBase
{
    private readonly Func<bool> action;
    public ActionNode(Func<bool> action) => this.action = action;
    public override bool Execute() => action();
}
