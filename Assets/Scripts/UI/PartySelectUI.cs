using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectUI : MonoBehaviour,IUIBase
{
    [System.Serializable]
    public struct CharacterButton
    {
        public Button button;
        public Image lockOverlay;
        public CharacterData data;
    }
    [Header("19개 버튼을 여기 배열에 등록하세요")]
    [SerializeField] private CharacterButton[] characterButtons;

    [SerializeField] private Button[] slots;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Button saveButton;

    private PartyDataManager partyData;
    private readonly List<CharacterData> selected = new();

    public int Priority => 1;

    private void Awake()
    {
        UIManager.Register(this);
    }
    public IEnumerator Initialize()
    {
        yield return GameManager.WaitUntilInitialized();
        partyData = DIContainer.Resolve<PartyDataManager>();
        foreach (var cb in characterButtons)
        {
            if (cb.button != null)
            {
                var local = cb;
                cb.button.onClick.AddListener(() => OnClickCharacter(local));
            }
        }
        // 슬롯 클릭 이벤트 등록 (해제용)
        for (int i = 0; i < slots.Length; i++)
        {
            int index = i;
            slots[i].onClick.AddListener(() => OnClickSlot(index));
        }
        // 저장 버튼 클릭 이벤트 등록
        saveButton.onClick.AddListener(SaveParty);
        // 해금 정보 갱신 이벤트 등록
        UnlockUI.OnUnlockUpdated -= RefreshVisual;
        UnlockUI.OnUnlockUpdated += RefreshVisual;
        // 이미 저장된 파티가 있으면 불러오기
        if (partyData.HasPartyData())
        {
            selected.Clear();
            selected.AddRange(partyData.GetSavedParty());
        }
        RefreshVisual();
    }

    private void OnClickCharacter(CharacterButton cb)
    {
        bool unlocked = SaveManager.Instance.GetCharacterUnlocked(cb.data.ID);
        if (!unlocked)
        {
            Debug.Log("해금되지 않은 캐릭터입니다.");
            return;
        }
        // 클릭 시 토글 방식
        if (selected.Contains(cb.data)) return;
        // 빈 슬롯 찾기
        if (selected.Count >= slots.Length)
        {
            Debug.Log("슬롯이 가득 찼습니다.");
            return;
        }
        selected.Add(cb.data);
        if (!PartyRules.Valid(selected, out var reason))
        {
            ShowFeedback(reason);
            selected.Remove(cb.data);
            return;
        }

        RefreshVisual();
    }
    private void OnClickSlot(int index)
    {
        if (index < 0 || index >= selected.Count)
        {
            Debug.Log("빈 슬롯입니다.");
            return;
        }

        var removed = selected[index];
        selected.Remove(removed);
        RefreshVisual();
    }
    private void SaveParty()
    {
        if (selected.Count == 0)
        {
            Debug.Log("공격대가 비어 있습니다.");
            return;
        }
        partyData.SaveParty(selected);
        Debug.Log("공격대가 저장되었습니다.");
    }
    private void RefreshVisual()
    {
        // 선택된 캐릭터는 버튼을 비활성화 (혹은 색상 변경)
        foreach (var cb in characterButtons)
        {
            bool unlocked = SaveManager.Instance.GetCharacterUnlocked(cb.data.ID);
            bool isSelected = selected.Contains(cb.data);

            if (cb.lockOverlay) cb.lockOverlay.gameObject.SetActive(!unlocked);
            if (cb.button) cb.button.interactable = unlocked && !isSelected;
        }
        // 슬롯 이미지 갱신
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < selected.Count && selected[i] != null)
            {
                // CharacterData에 IconSprite 추가 필요
                slotImages[i].sprite = selected[i].myImage;
                slotImages[i].gameObject.SetActive(true);
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].gameObject.SetActive(false);
            }
        }
    }

    private void ShowFeedback(string msg)
    {
        Debug.Log(msg);
    }

}
