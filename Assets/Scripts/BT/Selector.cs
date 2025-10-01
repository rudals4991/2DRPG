using System.Collections.Generic;
using UnityEngine;

public class Selector : NodeBase
{
    private readonly List<NodeBase> child;
    public Selector(List<NodeBase> child) => this.child = child;
    public override bool Execute()
    {
        foreach (var c in child)
        { 
            if(c.Execute()) return true;
        }
        return false;
    }
}
