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
    public static bool Valid(List<CharacterType> types, out string reason)
    { 
        reason = string.Empty;
        if (types is null || types.Count == 0)
        {
            reason = "공격대를 최소 1명 이상 선택해주세요"; 
            return false;
        }
        if (types.Count > MaxSize)
        { 
            reason = $"공격대는 최대 {MaxSize}명까지 가능합니다.";
            return false; 
        }
        if (types.Any(t => !AllowedRules.Contains(t)))
        { 
            reason = "몬스터/Boss는 공격대에 편성할 수 없습니다."; 
            return false;
        }
        if (types.Count != types.Distinct().Count())
        { 
            reason = "같은 직업군은 중복 선택할 수 없습니다.";
            return false; 
        }
        return true;
    }
}
