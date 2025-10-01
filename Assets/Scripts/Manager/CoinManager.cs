using System.Collections;
using UnityEngine;

public class CoinManager : MonoBehaviour, IManagerBase
{
    public int Priority => 3;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
    }
}
