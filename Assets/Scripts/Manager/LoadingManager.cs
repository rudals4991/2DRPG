using System.Collections;
using UnityEngine;

public class LoadingManager : MonoBehaviour, IManagerBase
{
    public int Priority => 4;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
        DIContainer.Register(this);
    }
}
