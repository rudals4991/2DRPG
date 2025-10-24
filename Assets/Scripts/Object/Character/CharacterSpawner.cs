using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    CharacterManager characterManager;
    [SerializeField] PlayerBase[] players;
    [SerializeField] CharacterBase[] monsters;
    private void Awake()
    {
        DIContainer.Register(this);
    }
    public void Initialize()
    { 
        SpawnPlayer();
        SpawnMonster();
    }
    void SpawnPlayer()
    {
        foreach (PlayerBase player in players)
        {
            var p = Instantiate(player, Vector3.zero, Quaternion.identity);
            p.Initialize();
        }
    }
    void SpawnMonster()
    {
        foreach (CharacterBase monster in monsters)
        {
            var m = Instantiate(monster, new Vector3(2, 0, 0), Quaternion.identity);
        }
    }
}
