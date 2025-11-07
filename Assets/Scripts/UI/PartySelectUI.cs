using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectUI : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterButton
    {
        public Button button;
        public CharacterData data;
    }
    [Header("19개 버튼을 여기 배열에 등록하세요")]
    [SerializeField] private CharacterButton[] characterButtons;

    private PartyDataManager partyData;
    private readonly List<CharacterData> selected = new();

    private IEnumerator Start()
    {
        // GameManager 초기화가 완료될 때까지 대기
        yield return GameManager.WaitUntilInitialized();

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
            selected.AddRange(partyData.GetSavedParty());
            RefreshVisual();
        }
    }

    private void OnClickCharacter(CharacterButton cb)
    {
        // 클릭 시 토글 방식
        if (selected.Contains(cb.data))
        {
            selected.Remove(cb.data);
        }
        else
        {
            selected.Add(cb.data);
        }

        // 규칙 최종 검증
        if (!PartyRules.Valid(selected, out var reason))
        {
            ShowFeedback(reason);
            selected.Remove(cb.data);   
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
            bool isSelected = selected.Contains(cb.data);
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
