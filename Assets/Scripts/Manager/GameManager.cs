using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;
    CharacterManager characterManager;
    TargetManager targetManager;
    CoinManager coinManager;
    LoadingManager loadingManager;
    PoolManager poolManager;
    UIManager uiManager;
    bool isInitialized = false;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
    }
    private void InitializeManagers()
    {
        audioManager ??= GetComponent<AudioManager>() ?? gameObject.AddComponent<AudioManager>();
        characterManager ??= GetComponent<CharacterManager>() ?? gameObject.AddComponent<CharacterManager>();
        targetManager ??= GetComponent<TargetManager>() ?? gameObject.AddComponent<TargetManager>();
        loadingManager ??= GetComponent<LoadingManager>() ?? gameObject.AddComponent<LoadingManager>();
        poolManager ??= GetComponent<PoolManager>() ?? gameObject.AddComponent<PoolManager>();
        uiManager ??= GetComponent<UIManager>() ?? gameObject.AddComponent<UIManager>();
        coinManager ??= GetComponent<CoinManager>() ?? gameObject.AddComponent<CoinManager>();
        StartCoroutine(Flag());
    }
    private IEnumerator Flag()
    {
        yield return StartCoroutine(ManagerInitializer.InitializeAll());
        isInitialized = true;
    }
    private void ExitManagers()
    { 
        ManagerInitializer.ExitAll();
    }
    private void Update()
    {
        if (!isInitialized) return;
        float dt = Time.deltaTime;
        targetManager.Tick(dt);
        characterManager.TickAll(dt);
    }
}
