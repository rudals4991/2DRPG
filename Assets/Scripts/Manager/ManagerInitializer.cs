using System.Collections;
using System.Linq;
using UnityEngine;

public static class ManagerInitializer
{
    public static IEnumerator InitializeAll()
    {
        var managers = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IManagerBase>().OrderBy(m => m.Priority).ToList();
        foreach (var manager in managers)
        {
            yield return manager.Initialize();
        }
    }
    public static void ExitAll()
    {
        var managers = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IManagerBase>().OrderBy(m => m.Priority).ToList();
        foreach (var manager in managers)
        {
            manager.Exit();
        }
    }
}
