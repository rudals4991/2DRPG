using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyDataManager : MonoBehaviour, IManagerBase
{
    List<CharacterData> savedParty = new();
    public int Priority => 8;

    public void Exit()
    {
        savedParty.Clear();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        LoadPartyFromSave();
    }
    public void SaveParty(List<CharacterData> selected)
    {
        savedParty = new List<CharacterData>(selected);
        var ids = selected.Select(c => c.ID).ToList();
        SaveManager.Instance.SetParty(ids);
    }

    public List<CharacterData> GetSavedParty() => savedParty;
    public bool HasPartyData() => savedParty != null && savedParty.Count > 0;
    private void LoadPartyFromSave()
    {
        savedParty.Clear();

        var ids = SaveManager.Instance.GetParty();

        if (ids == null || ids.Count == 0)
            return;

        CharacterManager cm = DIContainer.Resolve<CharacterManager>();

        foreach (var id in ids)
        {
            var data = cm.GetCharacterDataByID(id);
            if (data != null)
                savedParty.Add(data);
        }
    }
}
