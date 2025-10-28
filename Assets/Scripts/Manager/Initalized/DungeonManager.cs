using System.Collections;
using UnityEngine;

public class DungeonManager : MonoBehaviour, IManagerBase
{
    private PartyDataManager partyDataManager;
    private DungeonDataManager dungeonDataManager;
    private PoolManager poolManager;

    private DungeonData currentDungeon;
    public int Priority => 9;

    public void Exit()
    {
        ExitDungeon();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        partyDataManager = DIContainer.Resolve<PartyDataManager>();
        dungeonDataManager = DIContainer.Resolve<DungeonDataManager>();
        poolManager = DIContainer.Resolve<PoolManager>();
    }
    public void EnterDungeon(int dungeonId)
    {
        if (!partyDataManager.HasPartyData()) return;
        currentDungeon = dungeonDataManager.GetDungeonData(dungeonId);
        if (currentDungeon == null) return;
        SpawnParty();
        SpawnMonsters();
    }
    private void SpawnParty()
    {
        var party = partyDataManager.GetSavedParty();
        Vector3[] startPos = { new(-3, 0, 0), new(-2, 0, 0), new(-1, 0, 0) };

        for (int i = 0; i < party.Count && i < 3; i++)
        {
            poolManager.SpawnPlayer(party[i], startPos[i], Quaternion.identity);
        }
    }

    private void SpawnMonsters()
    {
        float baseX = 3f;
        foreach (var monster in currentDungeon.monsterList)
        {
            for (int i = 0; i < monster.count; i++)
            {
                poolManager.SpawnMonster(monster.monsterTypeIndex, new Vector3(baseX + i, 0, 0), Quaternion.identity);
            }
            baseX += monster.count + 1f;
        }
    }

    public void ExitDungeon()
    {
        poolManager.ReleaseAllPlayers();
        poolManager.ReleaseAllMonsters();
    }
}
