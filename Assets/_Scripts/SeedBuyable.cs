using UnityEngine;

public class SeedBuyable : MonoBehaviour
{
    public int cost;
    public GameObject seed;
    public InventoryManager inventoryManager;
    public void TryPurchase()
    {
        if(GameManager.Instance.coins < cost)
        {
            return;
        }

        GameManager.Instance.coins -= cost;
        GameManager.Instance.UpdateSigns();
        inventoryManager.AddSeed(seed);
    }
}
