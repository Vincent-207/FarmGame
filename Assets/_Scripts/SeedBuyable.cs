using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedBuyable : MonoBehaviour
{
    public int cost;
    public GameObject seed;
    public InventoryManager inventoryManager;
    [SerializeField]
    TMP_Text nameBox, costBox;
    [SerializeField]
    Image seedIcon;
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

    void Start()
    {
        SeedSelectable seedSelectable = seed.GetComponent<SeedSelectable>();
        nameBox.text = seedSelectable.plantName;
        costBox.text = String.Format("{0}", cost);
        seedIcon.sprite = seed.GetComponent<Image>().sprite;
    }
}
