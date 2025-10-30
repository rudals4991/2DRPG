using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : ObjectPoolBase
{
    int myPoolSize = 30;
    protected override void Awake()
    {
        DIContainer.Register(this as ArrowPool);
        SetPoolSize(myPoolSize);
    }
}
