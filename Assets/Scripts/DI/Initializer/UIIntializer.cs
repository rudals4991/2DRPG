using System.Collections;
using System.Linq;
using UnityEngine;

public static class UIIntializer
{
    public static IEnumerator InitializeAll()
    {
        Debug.Log("123");
        var uis = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IUIBase>().OrderBy(m => m.Priority).ToList();
        foreach (var ui in uis)
        {
            yield return ui.Initialize();
        }
    }
}
