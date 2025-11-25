using System.Collections;
using UnityEngine;

public interface IUIBase
{
    int Priority { get; }
    IEnumerator Initialize();
}
