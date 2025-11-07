using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
        foreach (var c in activeCharacters)
        {
            if (c != null && c.Status.IsAlive)
                c.Tick(dt);
        }
    }
    public bool TryUnlock(CharacterData data)
    {
        if (data == null) return false;
        if (data.IsUnlocked) return true;

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

    private void SaveUnlockState(CharacterData data)
    {
        PlayerPrefs.SetInt($"Unlock_{data.Name}", data.IsUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadUnlockData()
    {
        foreach (var data in allCharacterData)
        {
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
}
