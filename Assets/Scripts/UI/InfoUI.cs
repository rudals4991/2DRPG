using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour,IUIBase
{
    [SerializeField] GameObject panel;
    [SerializeField] Image image;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text type;
    [SerializeField] TMP_Text maxHp;
    [SerializeField] TMP_Text attackSpeed;
    [SerializeField] TMP_Text attackDamage;
    [SerializeField] TMP_Text attackRange;
    [SerializeField] Button levelupButton;

    CharacterData currentData;
    CharacterManager characterManager;

    public int Priority => 3;

    private void Awake()
    {
        UIManager.Register(this);
    }
    public IEnumerator Initialize()
    {
        yield return GameManager.WaitUntilInitialized();
        characterManager = DIContainer.Resolve<CharacterManager>();
        levelupButton.onClick.AddListener(OnClickLevelUp);
    }
    public void Show(CharacterData data)
    {
        currentData = data;
        UpdateUI();
        panel.SetActive(true);
    }

    public void OnClickLevelUp()
    {
        Debug.Log("Click");
        if (currentData is null)
        {
            Debug.Log("currentData is null");
            return;
        }
        if (characterManager.TryLevelUP(currentData)) UpdateUI();
    }
    public void UpdateUI()
    {
        int level = SaveManager.Instance.GetCharacterLevel(currentData.ID);
        CharacterStatus previewStatus = new CharacterStatus(currentData, level);
        image.sprite = currentData.myImage;
        nameText.text = $"{currentData.Name}  Lv.{level}";
        type.text = currentData.CharacterType.ToString();
        maxHp.text = previewStatus.MaxHP.ToString();
        attackSpeed.text = currentData.AttackSpeed.ToString();
        attackDamage.text = previewStatus.AttackDamage.ToString();
        attackRange.text = currentData.AttackRange.ToString();
    }
}
