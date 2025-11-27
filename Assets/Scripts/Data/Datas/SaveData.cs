using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // 세이브 구조 버전
    public int Version;

    // 재화
    public int Coin;

    // 캐릭터 정보
    public List<CharacterUnlockEntry> CharacterUnlocked;
    public List<CharacterLevelEntry> CharacterLevel;

    // 파티 정보
    public List<int> PartySlots;             // 슬롯에 들어가는 캐릭터 ID (-1 = 비어있음)

    // 던전 / 진행도 관련
    public int CurrentStage;                 // 현재 진행중인 스테이지
    public int HighestStage;                 // 최고 도달 스테이지

    // 옵션 (추후 UI 옵션 패널과 연동)
    public float BgmVolume;
    public float SfxVolume;
}
