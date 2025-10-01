using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour, IManagerBase
{
    public int Priority => 6;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
        DIContainer.Register(this);
    }
}
