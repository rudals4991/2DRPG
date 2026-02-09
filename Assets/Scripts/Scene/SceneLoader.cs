using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    const string TitleScene = "Title";
    const string ManagerScene = "Manager";
    const string UIScene = "UI";
    const string LobbyScene = "Lobby";
    const string DungeonScene = "Dungeon";

    static bool isBaseLoaded = false;
    public static IEnumerator LoadBaseScene()
    { 
        if(isBaseLoaded) yield break;
        yield return SceneManager.LoadSceneAsync(ManagerScene, LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync(UIScene, LoadSceneMode.Additive);
        isBaseLoaded = true;
    }
    public static IEnumerator LoadDungeon()
    {
        yield return LoadBaseScene();
        yield return SceneManager.LoadSceneAsync(DungeonScene, LoadSceneMode.Single);
    }
    public static IEnumerator LoadLobby()
    {
        yield return LoadBaseScene();
        yield return SceneManager.LoadSceneAsync(LobbyScene, LoadSceneMode.Single);
    }
    public static IEnumerator BackToLobby()
    {
        yield return LoadLobby();
    }
}
