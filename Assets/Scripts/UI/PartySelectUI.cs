using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectUI : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterButton
    {
        public Button button;
        public CharacterType type;
    }
    [Header("19개 버튼을 여기 배열에 등록하세요")]
    [SerializeField] private CharacterButton[] characterButtons;

    private PartyDataManager partyData;
    private readonly List<CharacterType> selected = new();

    private IEnumerator Start()
    {
        // GameManager 초기화가 완료될 때까지 대기
        yield return GameManager.WaitUntilInitialized();

        // 매니저 Resolve (모든 초기화 이후라 안전)
        partyData = DIContainer.Resolve<PartyDataManager>();

        // 버튼에 클릭 이벤트 등록
        foreach (var cb in characterButtons)
        {
            if (cb.button != null)
            {
                var local = cb;
                cb.button.onClick.AddListener(() => OnClickCharacter(local));
            }
        }

        // 이미 저장된 파티가 있으면 불러오기
        if (partyData.HasPartyData())
        {
            selected.Clear();
            selected.AddRange(partyData.GetSavedParty().Take(PartyRules.MaxSize));
            RefreshVisual();
        }
    }

    private void OnClickCharacter(CharacterButton cb)
    {
        // 클릭 시 토글 방식
        if (selected.Contains(cb.type))
        {
            selected.Remove(cb.type);
        }
        else
        {
            // 최대 인원 체크
            if (selected.Count >= PartyRules.MaxSize)
            {
                ShowFeedback($"최대 {PartyRules.MaxSize}명까지 선택할 수 있습니다.");
                return;
            }

            selected.Add(cb.type);
        }

        // 규칙 최종 검증
        if (!PartyRules.Valid(selected, out var reason))
        {
            ShowFeedback(reason);
            return;
        }

        // 저장
        partyData.SaveParty(selected);
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        // 선택된 캐릭터는 버튼을 비활성화 (혹은 색상 변경)
        foreach (var cb in characterButtons)
        {
            bool isSelected = selected.Contains(cb.type);
            if (cb.button)
            {
                cb.button.interactable = !isSelected;
            }
        }
    }

    private void ShowFeedback(string msg)
    {
        Debug.Log(msg);
    }
}
