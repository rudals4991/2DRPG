using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour, IManagerBase
{
    PartyDataManager partyDataManager;
    CharacterSpawner spawner;
    public int Priority => 10;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        partyDataManager = DIContainer.Resolve<PartyDataManager>();
        spawner = DIContainer.Resolve<CharacterSpawner>();
    }
    public void StartDungeon()
    {
        if (!partyDataManager.HasPartyData()) return;
        List<CharacterType> party = partyDataManager.GetSavedParty();
        spawner.SpawnParty(party);
        spawner.SpawnMonsters();
    }
}
