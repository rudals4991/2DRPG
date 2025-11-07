using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonData", menuName = "Game/DungeonData")]
public class DungeonData : ScriptableObject
{
    public int dungeonId;
    public string dungeonName;
    [Tooltip("이 던전 클리어 시 획득할 코인 수량")]
    public int rewardCoin;
    [Tooltip("이 던전에서 등장할 몬스터 목록")]
    public List<MonsterSpawnInfo> monsterList = new();
}

[System.Serializable]
public class MonsterSpawnInfo
{
    public CharacterData monsterData;
    public int count;
}
