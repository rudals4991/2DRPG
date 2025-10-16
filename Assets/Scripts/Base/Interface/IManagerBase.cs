using System.Collections;
using UnityEngine;

public interface IManagerBase
{
    int Priority { get;}
    IEnumerator Initialize();
    void Exit();
}
