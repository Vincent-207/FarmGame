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
    TMP_Text nameBox, costBox;
    [SerializeField] Image image;
    public bool TrySellCrop(int amount)
    {
        int cropCount; 
        GameManager.Instance.harvestedCrops.TryGetValue(cropType,out cropCount);
        if(cropCount < amount) return false;
        GameManager.Instance.harvestedCrops[cropType] = cropCount - amount;
        GameManager.Instance.coins += value * amount;
        return true;
    }
    public void TrySellCrop()
    {
        GameManager gameManager = GameManager.Instance;
        Debug.Log("trying sell");
        int cropCount; 
        gameManager.harvestedCrops.TryGetValue(cropType,out cropCount);
        if(cropCount <= 0) return;
        Debug.Log("Is good, selling");
        gameManager.harvestedCrops[cropType] = cropCount - 1;
        gameManager.coins += value;
        gameManager.UpdateSigns();
        
        return;
    }
    void Start()
    {
        image.sprite = Icon;
        nameBox.text = itemName;
        costBox.text = String.Format("{0}", value);
    }
}
