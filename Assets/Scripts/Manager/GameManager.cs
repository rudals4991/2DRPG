using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;
    CharacterManager characterManager;
    CoinManager coinManager;
    LoadingManager loadingManager;
    PoolManager poolManager;
    UIManager uiManager;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager On");
            InitializeManagers();
        }
    }
    private void InitializeManagers()
    {
        audioManager ??= GetComponent<AudioManager>() ?? gameObject.AddComponent<AudioManager>();
        characterManager ??= GetComponent<CharacterManager>() ?? gameObject.AddComponent<CharacterManager>();
        coinManager ??= GetComponent<CoinManager>() ?? gameObject.AddComponent<CoinManager>();
        loadingManager ??= GetComponent<LoadingManager>() ?? gameObject.AddComponent<LoadingManager>();
        poolManager ??= GetComponent<PoolManager>() ?? gameObject.AddComponent<PoolManager>();
        uiManager ??= GetComponent<UIManager>() ?? gameObject.AddComponent<UIManager>();
        StartCoroutine(ManagerInitializer.InitializeAll());
    }
    private void ExitManagers()
    { 
        ManagerInitializer.ExitAll();
    }
}
