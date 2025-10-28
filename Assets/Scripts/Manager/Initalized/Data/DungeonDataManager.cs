using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDataManager : MonoBehaviour,IManagerBase
{
    [SerializeField] private List<DungeonData> dungeonDatas = new();
    public int Priority => 8;

    public void Exit()
    {
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        
    }
    public DungeonData GetDungeonData(int id)
    {
        return dungeonDatas.Find(x => x.dungeonId == id);
    }
}
