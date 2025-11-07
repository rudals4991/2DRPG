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
    CoinManager coinManager;

    int aliveMonster = 0;
    int alivePlayer = 0;

    public int Priority => 9;

    public void Exit()
    {
        PlayerBase.OnPlayerDead -= PlayerDead;
        MonsterBase.OnMonsterDie -= MonsterDead;
        ExitDungeon();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        partyDataManager = DIContainer.Resolve<PartyDataManager>();
        dungeonDataManager = DIContainer.Resolve<DungeonDataManager>();
        poolManager = DIContainer.Resolve<PoolManager>();

        PlayerBase.OnPlayerDead -= PlayerDead;
        PlayerBase.OnPlayerDead += PlayerDead;
        MonsterBase.OnMonsterDie -= MonsterDead;
        MonsterBase.OnMonsterDie += MonsterDead;
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
        // 스폰 포인트 매니저 가져오기
        characterSpawnRule = DIContainer.Resolve<CharacterSpawnRule>();
        alivePlayer = 0;
        var frontList = new List<CharacterData>();
        var middleList = new List<CharacterData>();
        var backList = new List<CharacterData>();
        foreach (var data in party)
        {
            switch (data.CharacterType)
            {
                case CharacterType.Tanker:
                case CharacterType.Warrior:
                    frontList.Add(data);
                    break;

                case CharacterType.AD_DPS_Melee:
                case CharacterType.AD_DPS_Range:
                    middleList.Add(data);
                    break;

                case CharacterType.AP_DPS:
                case CharacterType.Buffer:
                case CharacterType.Healer:
                    backList.Add(data);
                    break;
            }
        }
        frontList.Sort(SortByPriority);
        middleList.Sort(SortByPriority);
        backList.Sort(SortByPriority);



        // 전방 소환
        for (int i = 0; i < frontList.Count; i++)
        {
            var data = frontList[i];
            var point = characterSpawnRule.GetSpawnPoint(data.CharacterType, i);
            poolManager.SpawnPlayer(data, point.position, Quaternion.Euler(0, 180, 0));
            alivePlayer++;
        }

        // 중앙 소환
        for (int i = 0; i < middleList.Count; i++)
        {
            var data = middleList[i];
            var point = characterSpawnRule.GetSpawnPoint(data.CharacterType, i);
            poolManager.SpawnPlayer(data, point.position, Quaternion.Euler(0, 180, 0));
            alivePlayer++;
        }

        // 후방 소환
        for (int i = 0; i < backList.Count; i++)
        {
            var data = backList[i];
            var point = characterSpawnRule.GetSpawnPoint(data.CharacterType, i);
            poolManager.SpawnPlayer(data, point.position, Quaternion.Euler(0, 180, 0));
            alivePlayer++;
        }

    }
    private int SortByPriority(CharacterData a, CharacterData b)
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
        return GetPriority(a.CharacterType).CompareTo(GetPriority(b.CharacterType));
    }

    private void SpawnMonsters()
    {
        monsterSpawnRule = DIContainer.Resolve<MonsterSpawnRule>();
        if (currentDungeon == null || currentDungeon.monsterList == null) return;
        aliveMonster = 0;
        int frontIndex = 0, middleIndex = 0, backIndex = 0;
        foreach (var monsterInfo in currentDungeon.monsterList)
        {
            for (int i = 0; i < monsterInfo.count; i++)
            {
                int lineType = GetMonsterLineType(monsterInfo.monsterData.CharacterType);
                int pointIndex = 0;

                switch (lineType)
                {
                    case 0: pointIndex = frontIndex++; break;
                    case 1: pointIndex = middleIndex++; break;
                    case 2: pointIndex = backIndex++; break;
                }

                var point = monsterSpawnRule.GetSpawnPoint(lineType, pointIndex);
                poolManager.SpawnMonster(monsterInfo.monsterData, point.position, Quaternion.identity);
                aliveMonster++;
            }
        }
    }
    // 몬스터 타입에 따라 배치 라인 결정 (임시 규칙)
    private int GetMonsterLineType(CharacterType type)
    {
        return type switch
        {
            CharacterType.Tanker => 0,                              // 전방
            CharacterType.Monster_Melee => 1,                       // 중앙
            CharacterType.Monster_Range or CharacterType.Boss => 2, // 후방
            _ => 1                                                  // 기본값: 중앙
        };
    }

    public void MonsterDead(MonsterBase monster)
    {
        aliveMonster--;
        if (aliveMonster <= 0) ClearDungeon();
    }
    public void PlayerDead(PlayerBase player)
    { 
        alivePlayer--;
        if (alivePlayer <= 0) FailDungeon(); 
    }

    public void ClearDungeon()
    {
        Debug.Log("던전 클리어");
        coinManager = DIContainer.Resolve<CoinManager>();
        int reward = currentDungeon.rewardCoin;
        coinManager.AddCoin(reward);
        ExitDungeon();
        //TODO: UI를 통한 연출 추가
    }
    public void FailDungeon()
    {
        ExitDungeon();
        Debug.Log("던전 실패");//TODO: 던전 실패 연출 추가
    }

    public void ExitDungeon()
    {
        poolManager.ReleaseAllPlayers();
        poolManager.ReleaseAllMonsters();
    }
}
