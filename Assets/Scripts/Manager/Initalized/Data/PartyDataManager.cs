using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyDataManager : MonoBehaviour, IManagerBase
{
    List<CharacterType> savedParty = new();
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
    public void SaveParty(List<CharacterType> selected)
    {
        savedParty = new List<CharacterType>(selected);
    }

    public List<CharacterType> GetSavedParty() => savedParty;

    public bool HasPartyData() => savedParty != null && savedParty.Count > 0;
}
