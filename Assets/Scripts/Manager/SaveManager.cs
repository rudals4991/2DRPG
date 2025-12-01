using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{
    private static SaveManager _instance;
    public static SaveManager Instance => _instance ??= new SaveManager();

    const string FileName = "Save.json";
    private SaveData data;
    public SaveData Data => data;
    private Dictionary<int, bool> unlockedDict;
    private Dictionary<int, int> levelDict;

    private string FilePath =>
        Path.Combine(Application.persistentDataPath, FileName);
    private SaveManager() { }
    public void Load()
    {
        if (!File.Exists(FilePath))
        {
            CreateDefaultData();
            Save();
        }
        else
        {
            string json = File.ReadAllText(FilePath);
            data = JsonUtility.FromJson<SaveData>(json);
            if (data == null)
            {
                CreateDefaultData();
                Save();
            }
        }

        ConvertListsToDictionaries();
    }
    public void Save()
    {
        ConvertDictionariesToLists();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
    }
    private void CreateDefaultData()
    {
        data = new SaveData()
        {
            Version = 1,
            Coin = 0,
            CharacterUnlocked = new List<CharacterUnlockEntry>(),
            CharacterLevel = new List<CharacterLevelEntry>(),
            PartySlots = new List<int>() { -1, -1, -1, -1 },
            CurrentStage = 1,
            HighestStage = 1,
            BgmVolume = 1f,
            SfxVolume = 1f
        };

        unlockedDict = new Dictionary<int, bool>();
        levelDict = new Dictionary<int, int>();
    }
    public int GetCharacterLevel(int id)
    {
        return levelDict != null && levelDict.TryGetValue(id, out int level)
            ? level
            : 1;
    }
    public bool GetCharacterUnlocked(int id)
    {
        return unlockedDict != null && unlockedDict.TryGetValue(id, out bool unlocked)
            ? unlocked
            : false;
    }
    public List<int> GetParty()
    {
        return data.PartySlots ?? new();
    }
    public void SetCharacterLevel(int id, int level)
    {
        levelDict[id] = level;
        Save();
    }
    public void SetCharacterUnlocked(int id, bool unlocked)
    {
        unlockedDict[id] = unlocked;
        Save();
    }
    public void SetParty(List<int> ids)
    {
        data.PartySlots = new(ids);
        Save();
    }
    public void SetCoin(int coin)
    { 
        data.Coin = coin;
        Save();
    }
    public void SetCurrentStage(int stage)
    {
        data.CurrentStage = stage;
        Save();
    }
    public void SetHighestStage(int stage)
    {
        data.HighestStage = Math.Max(data.HighestStage, stage);
        Save();
    }
    private void ConvertListsToDictionaries()
    {
        unlockedDict = new Dictionary<int, bool>();
        foreach (var e in data.CharacterUnlocked)
        {
            unlockedDict[e.ID] = e.Unlocked;
        }

        levelDict = new Dictionary<int, int>();
        foreach (var e in data.CharacterLevel)
        {
            levelDict[e.ID] = e.Level;
        }
    }
    private void ConvertDictionariesToLists()
    {
        data.CharacterUnlocked = new List<CharacterUnlockEntry>();
        foreach (var kvp in unlockedDict)
        {
            data.CharacterUnlocked.Add(new CharacterUnlockEntry
            {
                ID = kvp.Key,
                Unlocked = kvp.Value
            });
        }
        data.CharacterLevel = new List<CharacterLevelEntry>();
        foreach (var kvp in levelDict)
        {
            data.CharacterLevel.Add(new CharacterLevelEntry
            {
                ID = kvp.Key,
                Level = kvp.Value
            });
        }
    }
    public void ResetSave()
    {
        CreateDefaultData();
        Save();
    }
}
