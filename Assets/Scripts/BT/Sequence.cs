using System.Collections.Generic;
using UnityEngine;

public class Sequence : NodeBase
{
    private readonly List<NodeBase> child;
    public Sequence(List<NodeBase> child) => this.child = child;
    public override bool Execute()
    {
        foreach (var c in child)
        {
            if(!c.Execute()) return false;
        }
        return true;
    }
}
