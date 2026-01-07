using System;
using TMPro;
using UnityEngine;

public class SeedSelectable : MonoBehaviour
{
    [SerializeField]
    int quantity;
    public GameObject seedPrefab;
    public GameObject plantPrefab;
    public PlantType plantType;
    public String plantName;
    [SerializeField] TMP_Text quantityBox;
    public void SelectSeed()
    {
        PlantManager plantManager = PlantManager.Instance;
        plantManager.currentSelectedSeed = this;
        GameManager.Instance.seedPlacer.UpdateSelectedSeed();
    }
    void Start()
    {
        DoUpdate();
    }

    public void DoUpdate()
    {
        quantityBox.text = String.Format("x{0}", quantity);
        GameManager.Instance.UpdateSigns();
    }
    public void SetQuantity(int value)
    {
        quantity = value;
        DoUpdate();
    }
    public void IncrementQuantity()
    {
        quantity++;
        DoUpdate();
    }
    public void DecrementQuantity()
    {
        quantity--;
        DoUpdate();
    }
    public int GetQuantity()
    {
        return quantity;
    }
}
