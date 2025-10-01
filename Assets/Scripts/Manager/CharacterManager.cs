using System.Collections;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IManagerBase
{
    public int Priority => 2;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
        DIContainer.Register(this);
    }
}
