using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public Coin CoinPrefab;
    public Text TextCoin;

    private int collectedCoins;
    public int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }
        set
        {
            collectedCoins = value;
            TextCoin.text = collectedCoins.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
