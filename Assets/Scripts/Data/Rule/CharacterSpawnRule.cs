using UnityEngine;

public class CharacterSpawnRule : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnLine
    {
        public string name;
        public Transform[] points; // 각 라인 포인트들
    }

    [Header("전방 (Tanker, Warrior)")]
    public SpawnLine front;

    [Header("중앙 (AD DPS Melee, Range)")]
    public SpawnLine middle;

    [Header("후방 (AP, Buffer, Healer)")]
    public SpawnLine back;

    private void Awake()
    {
        DIContainer.Register(this);
    }

    // 직업군별 스폰 포인트 반환
    public Transform GetSpawnPoint(CharacterType type, int index)
    {
        Transform[] line = type switch
        {
            CharacterType.Tanker or CharacterType.Warrior => front.points,
            CharacterType.AD_DPS_Melee or CharacterType.AD_DPS_Range => middle.points,
            CharacterType.AP_DPS or CharacterType.Buffer or CharacterType.Healer => back.points,
            _ => middle.points
        };

        if (line == null || line.Length == 0)
            return transform;

        return line[Mathf.Clamp(index, 0, line.Length - 1)];
    }
}
