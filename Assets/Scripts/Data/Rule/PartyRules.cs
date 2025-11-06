using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PartyRules
{
    public const int MaxSize = 3;
    private static readonly HashSet<CharacterType> AllowedRules = new()
    {
        CharacterType.Tanker,CharacterType.Warrior,CharacterType.AD_DPS_Melee,
        CharacterType.AD_DPS_Range, CharacterType.AP_DPS, CharacterType.Healer, CharacterType.Buffer
    };
    public static bool Valid(List<CharacterData> list, out string reason)
    { 
        reason = string.Empty;
        if (list is null || list.Count == 0)
        {
            reason = "공격대를 최소 1명 이상 선택해주세요";
            return false;
        }

        if (list.Count > MaxSize)
        {
            reason = $"공격대는 최대 {MaxSize}명까지 가능합니다.";
            return false;
        }

        // CharacterData 내부 CharacterType 기준으로 검증
        if (list.Any(t => t == null))
        {
            reason = "선택된 캐릭터 중 데이터가 비어 있습니다.";
            return false;
        }

        if (list.Any(t => !AllowedRules.Contains(t.CharacterType)))
        {
            reason = "몬스터/Boss는 공격대에 편성할 수 없습니다.";
            return false;
        }

        // 같은 CharacterType 중복 선택 방지
        if (list.Select(t => t.CharacterType).Distinct().Count() != list.Count)
        {
            reason = "같은 직업군은 중복 선택할 수 없습니다.";
            return false;
        }
        return true;
    }
}
