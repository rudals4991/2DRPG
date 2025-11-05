using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DungeonStartUI : MonoBehaviour
{
    [Header("던전 시작 버튼")]
    [SerializeField] private Button startButton;

    [Header("던전 ID (임시로 0번 던전 사용)")]
    [SerializeField] private int dungeonId = 0;

    private PartyDataManager partyData;
    private StartManager startManager;

    private IEnumerator Start()
    {
        // 버튼 등록
        if (startButton != null)
            startButton.onClick.AddListener(OnClickStart);

        //  GameManager 초기화 완료 대기
        yield return GameManager.WaitUntilInitialized();

        // 초기화 완료 후 Resolve
        partyData = DIContainer.Resolve<PartyDataManager>();
        startManager = DIContainer.Resolve<StartManager>();
    }

    private void OnClickStart()
    {
        if (partyData == null || startManager == null)
        {
            Debug.Log("[DungeonStartButton] 매니저 초기화가 안 되어 있습니다.");
            return;
        }

        // 파티 선택 여부 확인
        if (!partyData.HasPartyData())
        {
            Debug.Log("[DungeonStartButton] 공격대를 먼저 선택해주세요!");
            return;
        }

        //  던전 시작 호출
        startManager.StartDungeon();
        Debug.Log($"[DungeonStartButton] 던전 {dungeonId}번 시작 요청!");
    }
}
