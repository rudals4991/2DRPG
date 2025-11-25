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
        SaveUnlockData();
        activeCharacters.Clear();
        allCharacterData.Clear();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        coinManager = DIContainer.Resolve<CoinManager>();
        LoadUnlockData();
        LoadCharacterLevels();
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
        if (data.IsUnlocked) return true;

        if (!CanUnlock(data))
        {
            Debug.Log($"[CharacterManager] 이전 캐릭터를 먼저 해금해야 합니다. ({data.CharacterType})");
            return false;
        }

        if (!coinManager.HasCoin(data.Cost))
        {
            Debug.Log($"[CharacterManager] 코인 부족. 필요: {data.Cost}");
            return false;
        }

        if (!coinManager.UseCoin(data.Cost))
            return false;

        data.IsUnlocked = true;
        SaveUnlockState(data);
        Debug.Log($"[CharacterManager] {data.Name} 해금 완료!");
        return true;
    }

    private bool CanUnlock(CharacterData data)
    {
        // 같은 역할(CharacterType) 내에서 이전 캐릭터가 존재해야 함
        var prev = allCharacterData.Find(x =>
            x.CharacterType == data.CharacterType && x.TypeIndex == data.TypeIndex - 1);

        if (prev == null) return true;

        return prev.IsUnlocked; // 이전 캐릭터가 해금돼 있어야 가능
    }

    private void SaveUnlockState(CharacterData data)
    {
        PlayerPrefs.SetInt($"Unlock_{data.Name}", data.IsUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadUnlockData()
    {
        foreach (var data in allCharacterData)
        {
            if (data.Cost == 0)
            {
                data.IsUnlocked = true;
                SaveUnlockState(data);
                continue;
            }
            data.IsUnlocked = PlayerPrefs.GetInt($"Unlock_{data.Name}", data.Cost == 0 ? 1 : 0) == 1;
        }
    }

    private void SaveUnlockData()
    {
        foreach (var data in allCharacterData)
        {
            SaveUnlockState(data);
        }
    }

    public bool TryLevelUP(CharacterData data)
    { 
        if(data is null) return false;
        if(!allCharacterData.Contains(data)) return false;
        if(data.level >= data.MaxLevel) return false;
        if (!coinManager.HasCoin(data.levelUpCost)) return false;
        if (!coinManager.UseCoin(data.levelUpCost)) return false;
        data.level++;
        data.MaxHp += data.HpAmount;
        data.AttackDamage += data.AttackAmount;
        SaveCharacterLevel(data);
        return true;
    }
    private void SaveCharacterLevel(CharacterData data)
    {
        PlayerPrefs.SetInt($"Level_{data.Name}", data.level);
        PlayerPrefs.Save();
    }

    public void LoadCharacterLevels()
    {
        foreach (var data in allCharacterData)
        {
            data.level = PlayerPrefs.GetInt($"Level_{data.Name}", 1);
        }
    }
}
