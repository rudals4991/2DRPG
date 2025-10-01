using System.Collections;
using UnityEngine;

public class PoolManager : MonoBehaviour, IManagerBase
{
    public int Priority => 5;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
        DIContainer.Register(this);
    }
}
