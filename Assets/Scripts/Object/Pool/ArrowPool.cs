using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : ObjectPoolBase
{
    int myPoolSize = 30;
    protected override void Awake()
    {
        base.Awake();
        SetPoolSize(myPoolSize);
    }
}
