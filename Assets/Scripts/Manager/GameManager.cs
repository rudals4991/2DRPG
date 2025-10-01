using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        StartCoroutine(ManagerInitializer.InitializeAll());
    }
    private void ExitManagers()
    { 
        ManagerInitializer.ExitAll();
    }
}
