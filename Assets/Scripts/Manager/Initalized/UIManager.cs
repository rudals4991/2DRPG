using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour, IManagerBase
{
    public int Priority => 3;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
    }
}
