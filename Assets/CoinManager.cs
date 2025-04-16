using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public UnityEvent<string> onCoinCountChange = new UnityEvent<string>();

    private const string CoinKey = "CoinCount";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        onCoinCountChange.Invoke(GetCoinCount().ToString());
    }

    public void AddCoins(int amount)
    {
        int currentCoins = PlayerPrefs.GetInt(CoinKey, 0);
        currentCoins += amount;
        PlayerPrefs.SetInt(CoinKey, currentCoins);
        PlayerPrefs.Save();

        onCoinCountChange.Invoke(currentCoins.ToString());
        Debug.Log("new coin value is : " + currentCoins);
    }

    public int GetCoinCount()
    {
        return PlayerPrefs.GetInt(CoinKey, 0);
    }
}
