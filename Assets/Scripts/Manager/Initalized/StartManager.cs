using System.Collections;
using UnityEngine;

public class StartManager : MonoBehaviour, IManagerBase
{
    public int Priority => 10;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
    }
}
