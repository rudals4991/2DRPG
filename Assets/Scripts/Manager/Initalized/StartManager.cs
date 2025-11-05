using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour, IManagerBase
{
    PartyDataManager partyDataManager;
    DungeonManager dungeonManager;
    public int Priority => 10;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        partyDataManager = DIContainer.Resolve<PartyDataManager>();
        dungeonManager = DIContainer.Resolve<DungeonManager>();
    }
    public void StartDungeon()
    {
        if (!partyDataManager.HasPartyData()) return;
        dungeonManager.EnterDungeon(0); //TODO: 던전 추가 후 보내는 시스템 구축
    }
}
