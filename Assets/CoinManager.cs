using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public UnityEvent<int> onCoinCountChange = new UnityEvent<int>();

    private int coinCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        onCoinCountChange.Invoke(coinCount);
        Debug.Log("new coin value is : " + coinCount);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
