using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IManagerBase
{
    private readonly List<CharacterBase> characters = new();
    public IReadOnlyList<CharacterBase> AllCharacters => characters;
    public int Priority => 2;
    CharacterSpawner spawner;
    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
        DIContainer.Register(this);
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
    }
    public void TickAll(float dt)
    {
        foreach (var character in characters)
        {
            if(character.Status.IsAlive) character.Tick(dt);
        }
    }
}
