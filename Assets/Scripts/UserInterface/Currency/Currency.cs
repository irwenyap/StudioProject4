using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public Text coinText;
    public Text gemText;
    public Text shardText;

    public int coins;
    public int gems;
    public int shards;

    private void Start()
    {
        coins = 0;
        gems = 0;
        shards = 0;
        coinText.text = coins.ToString();
        gemText.text = gems.ToString();
        shardText.text = shards.ToString();
    }

    public void AddCoins(int coinAmount)
    {
        coins += coinAmount;
        coinText.text = coins.ToString();
    }

    public void DeductCoins(int coinAmount)
    {
        coins -= coinAmount;
        coinText.text = coins.ToString();
    }

    public void AddGems(int gemAmount)
    {
        gems += gemAmount;
        gemText.text = gems.ToString();
    }

    public void DeductGems(int gemAmount)
    {
        gems -= gemAmount;
        gemText.text = gems.ToString();
    }

    public void AddShards(int shardAmount)
    {
        shards += shardAmount;
        shardText.text = shards.ToString();
    }

    public void DeductShards(int shardAmount)
    {
        shards -= shardAmount;
        shardText.text = shards.ToString();
    }
}
