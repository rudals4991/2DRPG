using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PoolManager poolManager;                //1
    AudioManager audioManager;              //2
    UIManager uiManager;                    //3
    CoinManager coinManager;                //4
    CharacterManager characterManager;      //5
    TargetManager targetManager;            //6
    PartyDataManager partyDataManager;      //7
    DungeonDataManager dungeonDataManager;  //8
    DungeonManager dungeonManager;          //9
    StartManager startManager;              //10
    LoadingManager loadingManager;          //11
    RangeObjectManager rangeObjectManager;  //12
    
    
    bool isInitialized = false;
    public bool IsInitialized => isInitialized;

    public static event Action Initialized;
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
        poolManager ??= GetComponent<PoolManager>() ?? gameObject.AddComponent<PoolManager>();
        audioManager ??= GetComponent<AudioManager>() ?? gameObject.AddComponent<AudioManager>();
        uiManager ??= GetComponent<UIManager>() ?? gameObject.AddComponent<UIManager>();
        coinManager ??= GetComponent<CoinManager>() ?? gameObject.AddComponent<CoinManager>();
        characterManager ??= GetComponent<CharacterManager>() ?? gameObject.AddComponent<CharacterManager>();
        targetManager ??= GetComponent<TargetManager>() ?? gameObject.AddComponent<TargetManager>();
        partyDataManager ??= GetComponent<PartyDataManager>() ?? gameObject.AddComponent<PartyDataManager>();
        dungeonDataManager ??= GetComponent<DungeonDataManager>() ?? gameObject.AddComponent<DungeonDataManager>();
        dungeonManager ??= GetComponent<DungeonManager>() ?? gameObject.AddComponent<DungeonManager>();
        startManager ??= GetComponent<StartManager>() ?? gameObject.AddComponent<StartManager>();
        loadingManager ??= GetComponent<LoadingManager>() ?? gameObject.AddComponent<LoadingManager>();
        rangeObjectManager ??= GetComponent<RangeObjectManager>() ?? gameObject.AddComponent<RangeObjectManager>();
        StartCoroutine(Flag());
    }
    private IEnumerator Flag()
    {
        yield return StartCoroutine(ManagerInitializer.InitializeAll());
        isInitialized = true;
        Initialized?.Invoke();
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
        rangeObjectManager.Tick(dt);

    }
    public static IEnumerator WaitUntilInitialized()
    {
        while (Instance == null || !Instance.IsInitialized)
            yield return null;
    }
}
