using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static readonly List<IUIBase> uiList = new();
    public static void Register(IUIBase ui)
    {
        if(!uiList.Contains(ui)) uiList.Add(ui);
    }
    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return GameManager.WaitUntilInitialized();
        foreach (IUIBase ui in uiList.OrderBy(u => u.Priority))
        {
            yield return ui.Initialize();
        }
    }
}
