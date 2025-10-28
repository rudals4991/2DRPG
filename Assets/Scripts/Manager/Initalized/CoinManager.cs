using System.Collections;
using UnityEngine;

public class CoinManager : MonoBehaviour, IManagerBase
{
    public int Priority => 4;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
    }
}
