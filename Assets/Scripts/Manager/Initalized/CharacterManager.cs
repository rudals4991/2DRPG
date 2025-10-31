using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IManagerBase
{
    private readonly List<CharacterBase> characters = new();
    public IReadOnlyList<CharacterBase> AllCharacters => characters;
    public int Priority => 5;
    CharacterSpawner spawner;
    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        spawner = DIContainer.Resolve<CharacterSpawner>();
        spawner.Initialize();
    }
    public void Register(CharacterBase c)
    {
        if (!characters.Contains(c))
            characters.Add(c);
        Debug.Log($"[CharacterManager] Registered: {c.name}");
    }
    public void Unregister(CharacterBase c)
    {
        characters.Remove(c);
        foreach (var other in characters)
        {
            if (other.Target == c)
            {
                other.SetTarget(null);
            }
        }
    }
    public void TickAll(float dt)
    {
        for (int i = characters.Count - 1; i >= 0; i--)
        {
            var c = characters[i];
            if (c == null || !c.gameObject.activeInHierarchy || !c.Status.IsAlive)
            {
                characters.RemoveAt(i);
                continue;
            }
            c.Tick(dt);
        }
    }
}
