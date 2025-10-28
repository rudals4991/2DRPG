using System.Collections;
using UnityEngine;

public class LoadingManager : MonoBehaviour, IManagerBase
{
    public int Priority => 11;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
    }
}
