using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour, IManagerBase
{
    PartyDataManager partyDataManager;
    DungeonDataManager dungeonDataManager;
    PoolManager poolManager;
    CharacterSpawnRule characterSpawnRule;
    MonsterSpawnRule monsterSpawnRule;
    DungeonData currentDungeon;

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
        if (party == null || party.Count == 0) return;
        var frontList = new List<CharacterType>();
        var middleList = new List<CharacterType>();
        var backList = new List<CharacterType>();
        foreach (var type in party)
        {
            switch (type)
            {
                case CharacterType.Tanker:
                case CharacterType.Warrior:
                    frontList.Add(type);
                    break;
                case CharacterType.AD_DPS_Melee:
                case CharacterType.AD_DPS_Range:
                    middleList.Add(type);
                    break;
                case CharacterType.AP_DPS:
                case CharacterType.Buffer:
                case CharacterType.Healer:
                    backList.Add(type);
                    break;
            }
        }
        frontList.Sort(SortByPriority);
        middleList.Sort(SortByPriority);
        backList.Sort(SortByPriority);

        // 스폰 포인트 매니저 가져오기
        characterSpawnRule = DIContainer.Resolve<CharacterSpawnRule>();

        // 전방 소환
        for (int i = 0; i < frontList.Count; i++)
        {
            var type = frontList[i];
            var point = characterSpawnRule.GetSpawnPoint(type, i);
            poolManager.SpawnPlayer(type, point.position, Quaternion.Euler(0, 180, 0));
        }

        // 중앙 소환
        for (int i = 0; i < middleList.Count; i++)
        {
            var type = middleList[i];
            var point = characterSpawnRule.GetSpawnPoint(type, i);
            poolManager.SpawnPlayer(type, point.position, Quaternion.Euler(0, 180, 0));
        }

        // 후방 소환
        for (int i = 0; i < backList.Count; i++)
        {
            var type = backList[i];
            var point = characterSpawnRule.GetSpawnPoint(type, i);
            poolManager.SpawnPlayer(type, point.position, Quaternion.Euler(0, 180, 0));
        }

    }
    private int SortByPriority(CharacterType a, CharacterType b)
    {
        int GetPriority(CharacterType t) => t switch
        {
            CharacterType.Tanker => 1,
            CharacterType.Warrior => 2,
            CharacterType.AD_DPS_Melee => 3,
            CharacterType.AD_DPS_Range => 4,
            CharacterType.AP_DPS => 5,
            CharacterType.Buffer => 6,
            CharacterType.Healer => 7,
            _ => 999
        };
        return GetPriority(a).CompareTo(GetPriority(b));
    }

    private void SpawnMonsters()
    {
        monsterSpawnRule = DIContainer.Resolve<MonsterSpawnRule>();
        if (currentDungeon == null || currentDungeon.monsterList == null) return;
        int frontIndex = 0, middleIndex = 0, backIndex = 0;
        foreach (var monsterInfo in currentDungeon.monsterList)
        {
            for (int i = 0; i < monsterInfo.count; i++)
            {
                int lineType = GetMonsterLineType(monsterInfo.monsterTypeIndex);
                int pointIndex = 0;

                switch (lineType)
                {
                    case 0: pointIndex = frontIndex++; break;
                    case 1: pointIndex = middleIndex++; break;
                    case 2: pointIndex = backIndex++; break;
                }

                var point = monsterSpawnRule.GetSpawnPoint(lineType, pointIndex);
                poolManager.SpawnMonster(monsterInfo.monsterTypeIndex, point.position, Quaternion.identity);
            }
        }
    }
    // 몬스터 타입에 따라 배치 라인 결정 (임시 규칙)
    private int GetMonsterLineType(int monsterTypeIndex)
    {
        return monsterTypeIndex switch
        {
            0 or 1 or 4 or 5 => 0, // 전방
            2 or 3 => 2,           // 후방
            6 or 7 or 8 => 1,      // 중앙
            _ => 1                 // 기본값: 중앙
        };
    }

    public void ExitDungeon()
    {
        poolManager.ReleaseAllPlayers();
        poolManager.ReleaseAllMonsters();
    }
}
