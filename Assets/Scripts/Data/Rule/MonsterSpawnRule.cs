using UnityEngine;

public class MonsterSpawnRule : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnLine
    {
        public string name;
        public Transform[] points;
    }

    [Header("전방 (탱커형, 돌격형 몬스터)")]
    public SpawnLine front;

    [Header("중앙 (근접, 원거리 공격 몬스터)")]
    public SpawnLine middle;

    [Header("후방 (마법형, 보조형 몬스터)")]
    public SpawnLine back;

    private void Awake()
    {
        DIContainer.Register(this);
    }

    public Transform GetSpawnPoint(int lineType, int index)
    {
        Transform[] line = lineType switch
        {
            0 => front.points,   // 전방
            1 => middle.points,  // 중앙
            2 => back.points,    // 후방
            _ => middle.points
        };

        if (line == null || line.Length == 0)
            return transform;

        return line[Mathf.Clamp(index, 0, line.Length - 1)];
    }
}
