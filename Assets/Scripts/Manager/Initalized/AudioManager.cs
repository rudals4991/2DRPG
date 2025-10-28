using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour, IManagerBase
{
    public int Priority => 2;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
    }
}
