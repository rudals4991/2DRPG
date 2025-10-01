using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour, IManagerBase
{
    public int Priority => 1;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
        DIContainer.Register(this);
    }
}
