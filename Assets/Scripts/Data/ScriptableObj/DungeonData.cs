using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonData", menuName = "Game/DungeonData")]
public class DungeonData : ScriptableObject
{
    public int dungeonId;
    public string dungeonName;

    [Tooltip("이 던전에서 등장할 몬스터 목록")]
    public List<MonsterSpawnInfo> monsterList = new();
}

[System.Serializable]
public class MonsterSpawnInfo
{
    public int monsterTypeIndex;
    public int count;
}
