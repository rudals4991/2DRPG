using System;
using System.Collections;
using UnityEngine;

public class CoinManager : MonoBehaviour, IManagerBase
{
    int currentCoin;
    public int Priority => 4;
    public static event Action<int> OnCoinChanged;

    public void Exit()
    {
        SaveCoin();
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        LoadCoin();
    }
    public void AddCoin(int coin)
    {
        if (coin <= 0) return;
        currentCoin += coin;
        OnCoinChanged?.Invoke(currentCoin);
        SaveCoin();
    }
    public bool UseCoin(int coin)
    { 
        if(coin <= 0) return false;
        if (currentCoin < coin) return false;
        currentCoin -= coin;
        OnCoinChanged?.Invoke(currentCoin);
        SaveCoin();
        return true;
    }
    public bool HasCoin(int coin)
    {
        return currentCoin >= coin;
    }
    public int GetCurrentCoin() => currentCoin;
    public void SaveCoin()
    {
        PlayerPrefs.SetInt("PlayerCoin", currentCoin);
        PlayerPrefs.Save();
        Debug.Log($"현재 코인 = {currentCoin}");
    }
    public void LoadCoin()
    {
        currentCoin = PlayerPrefs.GetInt("PlayerCoin", 0);
    }
}
