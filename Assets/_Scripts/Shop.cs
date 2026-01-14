using UnityEngine;

public class Shop : MonoBehaviour
{
    public void Sell()
    {
        GameManager gameManager = GameManager.Instance;
        int cropCount = gameManager.cropCount;
        if(cropCount <= 0)
        {
            Debug.LogWarning("Not enough crops to sell.");
            return;
        }
        gameManager.cropCount--;
        gameManager.coins += 2;
        gameManager.UpdateSigns();
    }
    public void Buy()
    {
        GameManager gameManager = GameManager.Instance;
        int coinCount = gameManager.coins;
        if(coinCount <= 0)
        {
            Debug.LogWarning("Not enough coins to buy.");
            return;
        }
        gameManager.coins--;
        gameManager.seeds += 1;
        gameManager.UpdateSigns();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Debug.Log("Enabled!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
