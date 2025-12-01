using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IManagerBase
{
    CoinManager coinManager;
    readonly List<CharacterBase> activeCharacters = new();
    readonly List<CharacterData> allCharacterData = new();
    public IReadOnlyList<CharacterBase> AllCharacters => activeCharacters;
    public int Priority => 6;
    public void Exit()
    {
        activeCharacters.Clear();
        allCharacterData.Clear();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        coinManager = DIContainer.Resolve<CoinManager>();
    }
    public void Register(CharacterBase c)
    {
        if (!activeCharacters.Contains(c)) activeCharacters.Add(c);
    }
    public void Unregister(CharacterBase c)
    {
        activeCharacters.Remove(c);
        foreach (var other in activeCharacters)
        {
            if (other.Target == c) other.SetTarget(null);
        }
    }
    public void RegisterCharacterData(CharacterData data)
    {
        if (!allCharacterData.Contains(data)) allCharacterData.Add(data);
    }

    public void TickAll(float dt)
    {
        for (int i = activeCharacters.Count - 1; i >= 0; i--)
        {
            var c = activeCharacters[i];
            if (c == null) continue;
            if (c.Status.IsAlive)
                c.Tick(dt);
        }
    }
    public bool TryUnlock(CharacterData data)
    {
        if (data == null) return false;

        bool unlocked = SaveManager.Instance.GetCharacterUnlocked(data.ID);
        if (unlocked) return true;

        if (!CanUnlock(data))
        {
            Debug.Log($"[CharacterManager] 이전 캐릭터를 먼저 해금해야 합니다. ({data.CharacterType})");
            return false;
        }

        if (!coinManager.HasCoin(data.Cost)) return false;
        if (!coinManager.UseCoin(data.Cost)) return false;

        SaveManager.Instance.SetCharacterUnlocked(data.ID, true);

        Debug.Log($"[CharacterManager] {data.Name} 해금 완료!");
        return true;
    }

    private bool CanUnlock(CharacterData data)
    {
        var prev = allCharacterData.Find(x =>
            x.CharacterType == data.CharacterType &&
            x.TypeIndex == data.TypeIndex - 1);

        if (prev == null) return true;

        return SaveManager.Instance.GetCharacterUnlocked(prev.ID);
    }

    public bool TryLevelUP(CharacterData data)
    {
        if (data == null) return false;

        int level = SaveManager.Instance.GetCharacterLevel(data.ID);
        if (level >= data.MaxLevel) return false;

        if (!coinManager.HasCoin(data.LevelUpCost)) return false;
        if (!coinManager.UseCoin(data.LevelUpCost)) return false;

        level++;
        SaveManager.Instance.SetCharacterLevel(data.ID, level);

        return true;
    }
    public CharacterData GetCharacterDataByID(int id)
    {
        return allCharacterData.Find(c => c.ID == id);
    }
}
