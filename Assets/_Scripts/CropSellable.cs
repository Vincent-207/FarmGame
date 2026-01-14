using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CropSellable : MonoBehaviour
{
    public CropType cropType;
    public int value;
    [SerializeField]
    String itemName;
    [SerializeField]
    Sprite Icon;
    [SerializeField]
    TMP_Text nameBox, costBox, quantityBox;
    [SerializeField] Image image;
    public bool TrySellCrop(int amount)
    {
        double cropCount; 
        GameManager.Instance.harvestedCrops.TryGetValue(cropType, out cropCount);
        if(cropCount < amount) return false;
        GameManager.Instance.harvestedCrops[cropType] = cropCount - amount;
        GameManager.Instance.coins += value * amount;
        return true;
    }
    public void TrySellCrop()
    {
        GameManager gameManager = GameManager.Instance;
        Debug.Log("trying sell");
        double cropCount; 
        gameManager.harvestedCrops.TryGetValue(cropType,out cropCount);
        if(cropCount <= 0) return;
        Debug.Log("Is good, selling");
        gameManager.harvestedCrops[cropType] = 0;
        gameManager.coins +=  value * (int) cropCount;
        gameManager.UpdateSigns();
        UpdateQuantity();
        return;
    }
    public void UpdateQuantity()
    {
        double quantity  = 0;
        GameManager.Instance.harvestedCrops.TryGetValue(cropType, out quantity);
        quantityBox.text = String.Format("x{0}", (int) quantity);
    }
    void Start()
    {
        image.sprite = Icon;
        nameBox.text = itemName;
        costBox.text = String.Format("{0}", value);
        UpdateQuantity();
    }
}
