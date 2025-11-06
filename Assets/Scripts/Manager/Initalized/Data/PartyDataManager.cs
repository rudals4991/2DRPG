using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyDataManager : MonoBehaviour, IManagerBase
{
    List<CharacterData> savedParty = new();
    public int Priority => 7;

    public void Exit()
    {
        savedParty.Clear();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
    }
    public void SaveParty(List<CharacterData> selected)
    {
        savedParty = new List<CharacterData>(selected);
    }

    public List<CharacterData> GetSavedParty() => savedParty;

    public bool HasPartyData() => savedParty != null && savedParty.Count > 0;
}
