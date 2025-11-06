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

        if (line == null || line.Length == 0) return null;

        int pointIndex = Mathf.Clamp(index, 0, line.Length - 1);
        Transform basePoint = line[pointIndex];

        // 몬스터는 반대 방향(왼쪽)으로 간격
        Vector3 offset = new Vector3(-index * 0.5f, 0f, 0f);

        GameObject temp = new GameObject("TempMonsterSpawn");
        temp.transform.position = basePoint.position + offset;
        return temp.transform;
    }
}
