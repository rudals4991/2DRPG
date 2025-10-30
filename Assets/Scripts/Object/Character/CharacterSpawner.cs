using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    PoolManager poolManager;
    CharacterManager characterManager;
    [SerializeField] Transform playerSpawnRoot;
    [SerializeField] Transform monsterSpawnRoot;
    private void Awake()
    {
        DIContainer.Register(this);
    }
    public void Initialize()
    { 
        poolManager = DIContainer.Resolve<PoolManager>();
        characterManager = DIContainer.Resolve<CharacterManager>();
    }
    public void SpawnParty(List<CharacterType> partyTypes)
    {
        for (int i = 0; i < partyTypes.Count; i++)
        {
            CharacterType type = partyTypes[i];
            Vector3 spawnPos = playerSpawnRoot ? playerSpawnRoot.position + new Vector3(i * 2f, 0, 0) :
                new Vector3(i * 2f, 0, 0);
            Quaternion rot = Quaternion.Euler(0, 180, 0);
            GameObject obj = poolManager.SpawnPlayer(type, spawnPos, rot);
            if (obj == null) continue;
            if (obj.TryGetComponent(out PlayerBase player))
            {
                player.Initialize();
                characterManager.Register(player);
            }
        }
    }
    public void SpawnMonsters()
    {
        int[] monsterTypes = { 0,};
        for (int i = 0; i < monsterTypes.Length; i++)
        {
            Vector3 spawnPos = monsterSpawnRoot ?monsterSpawnRoot.position + new Vector3(i * 3f, 0, 0) :
                new Vector3(15 + i * 2f, 0, 0);

            GameObject obj = poolManager.SpawnMonster(monsterTypes[i], spawnPos, Quaternion.identity);
            if (obj.TryGetComponent(out CharacterBase monster))
            {
                monster.Initialize();
                characterManager.Register(monster);
            }
        }
    }
}
