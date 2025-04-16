using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public UnityEvent<string> onCoinCountChange = new UnityEvent<string>();

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
        onCoinCountChange.Invoke(coinCount.ToString());
        Debug.Log("new coin value is : " + coinCount);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
