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
    }

    public IEnumerator Initialize()
    {
        DIContainer.Register(this);
        yield return null;
        LoadCoin();
        OnCoinChanged?.Invoke(currentCoin);
    }
    public void AddCoin(int coin)
    {
        if (coin <= 0) return;
        currentCoin += coin;
        SaveManager.Instance.SetCoin(currentCoin);
        OnCoinChanged?.Invoke(currentCoin);
    }
    public bool UseCoin(int coin)
    { 
        if (currentCoin < coin) return false;
        currentCoin -= coin;
        SaveManager.Instance.SetCoin(currentCoin);
        OnCoinChanged?.Invoke(currentCoin);
        return true;
    }
    public bool HasCoin(int coin)
    {
        return currentCoin >= coin;
    }
    public int GetCurrentCoin() => currentCoin;
    public void LoadCoin()
    {
        currentCoin = SaveManager.Instance.Data.Coin;
    }
}
