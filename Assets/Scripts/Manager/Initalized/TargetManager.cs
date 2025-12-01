using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour, IManagerBase
{
    CharacterManager characterManager;
    public int Priority => 7;
    private float updateInterval = 0.25f;
    private float timer;
    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
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

        foreach (var character in characters)
        {
            // 1. 각 캐릭터 태그 확인

            if (!character.Status.IsAlive)
            {
                character.SetTarget(null);
                continue;
            }

            // 2. 기존 타겟 유지 조건 체크
            if (character.Target != null &&
                character.Target.Status.IsAlive &&
                IsEnemy(character, character.Target)) continue;

            CharacterBase nearest = null;
            float minDist = float.MaxValue;

            foreach (var potential in characters)
            {
                if (potential == character) continue;
                if (!potential.Status.IsAlive) continue;

                bool enemyCheck = IsEnemy(character, potential);

                float dist = Vector2.Distance(character.transform.position, potential.transform.position);

                if (!enemyCheck) continue;

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
