using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockUI : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterButton
    {
        public Button button;
        public Image lockOverlay;   // 잠금 패널
        public CharacterData data;
    }
    [SerializeField] CharacterButton[] characterButtons;
    [SerializeField] GameObject infoPanel;
    [SerializeField] Image image;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text type;
    [SerializeField] TMP_Text maxHp;
    [SerializeField] TMP_Text attackSpeed;
    [SerializeField] TMP_Text attackDamage;
    [SerializeField] TMP_Text attackRange;
    CharacterManager characterManager;
    public static event Action OnUnlockUpdated;

    private IEnumerator Start()
    {
        yield return GameManager.WaitUntilInitialized();
        characterManager = DIContainer.Resolve<CharacterManager>();
        characterManager.LoadUnlockData();
        foreach (var cb in characterButtons)
        {
            characterManager.RegisterCharacterData(cb.data);
            var btn = cb.button;
            var data = cb.data;
            var overlay = cb.lockOverlay;
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    OnClickCharacter(new CharacterButton
                    {
                        button = btn,
                        data = data,
                        lockOverlay = overlay
                    });
                });
            }
        }
        RefreshVisual();
    }
    private void OnClickCharacter(CharacterButton cb)
    {
        if (cb.data.IsUnlocked)
        {
            ShowCharacterInfo(cb.data);
            return;
        }

        ShowUnlock(cb);
    }
    private void RefreshVisual()
    {
        foreach (var cb in characterButtons)
        {
            bool unlocked = cb.data.IsUnlocked;
            if (cb.lockOverlay != null) cb.lockOverlay.gameObject.SetActive(!unlocked);
        }
    }
    private void ShowUnlock(CharacterButton cb)
    {
        //  실제 UI로는 팝업창을 띄우겠지만, 지금은 로그와 입력으로 대체
        Debug.Log($"[{cb.data.Name}] 캐릭터를 {cb.data.Cost} 코인으로 해금하시겠습니까? (Y/N)");

        // 지금은 단순히 자동 확인 처리 (UI 붙이면 여기에 Yes/No 연결)
        StartCoroutine(AutoConfirmUnlock(cb));
    }
    private IEnumerator AutoConfirmUnlock(CharacterButton cb)
    {
        // 실제 게임에선 팝업의 버튼 클릭으로 분기되겠지만
        yield return new WaitForSeconds(0.5f); // 테스트용 대기

        bool confirmed = true; // 여기선 자동으로 ‘Yes’로 처리
        if (!confirmed) yield break;

        if (!characterManager.TryUnlock(cb.data))
        {
            Debug.Log("해금 조건을 만족하지 못했습니다.");
            yield break;
        }

        Debug.Log($"{cb.data.Name}이(가) 해금되었습니다!");
        RefreshVisual();
        OnUnlockUpdated?.Invoke();
    }

    private void ShowCharacterInfo(CharacterData data)
    {
        infoPanel.SetActive(true);
        image.sprite = data.myImage;
        nameText.text = data.name;
        type.text = data.CharacterType.ToString();
        maxHp.text = data.MaxHp.ToString();
        attackSpeed.text = data.AttackSpeed.ToString();
        attackDamage.text = data.AttackDamage.ToString();
        attackRange.text = data.AttackRange.ToString();
    }
}
