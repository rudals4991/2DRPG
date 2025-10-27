using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour, IManagerBase
{
    CharacterManager characterManager;
    public int Priority => 3;
    private float updateInterval = 0.25f;
    private float timer;
    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        yield return null;
        DIContainer.Register(this);
        characterManager = DIContainer.Resolve<CharacterManager>();
    }
    public void Tick(float dt)
    {
        timer -= dt;
        if (timer <= 0f)
        {
            timer = updateInterval;
            UpdateTarget();
        }
    }
    public void UpdateTarget()
    {
        if (characterManager is null) return;
        var characters = characterManager.AllCharacters;
        if (characters is null) Debug.Log("characters is null");
        foreach (var character in characters)
        {
            if (!character.Status.IsAlive)
            {
                character.SetTarget(null);
                continue;
            }

            if (character.Target != null && character.Target.Status.IsAlive && 
                IsEnemy(character, character.Target)) continue;

            CharacterBase nearest = null;
            float minDist = float.MaxValue;

            foreach (var potential in characters)
            {
                if (potential == character) continue;
                if (!potential.Status.IsAlive) continue;
                if (!IsEnemy(character, potential)) continue;

                float dist = Vector2.Distance(character.transform.position, potential.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = potential;
                }
            }

            character.SetTarget(nearest);
        }
    }
    private bool IsEnemy(CharacterBase a, CharacterBase b)
    {
        if (a.CompareTag("Player") && b.CompareTag("Monster")) return true;
        if (a.CompareTag("Monster") && b.CompareTag("Player")) return true;
        return false;
    }
}
