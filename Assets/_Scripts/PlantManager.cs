using UnityEngine;
using UnityEngine.Events;

public class PlantManager : MonoBehaviour
{
    public Disease[] diseasePrefabs;
    public GameObject[] diseaseOverlays;
    public static PlantManager Instance {get; private set;}
    // current selected plant for inspection
    public Plant currentPlant;
    // current selected seed for placing
    public SeedSelectable currentSelectedSeed;
    public SeedSelectable[] selectableSeeds;
    [SerializeField] GameObject inventoryHolder;
    public InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }         

        
    }

    void UpdateSelectableSeeds()
    {
        selectableSeeds = inventoryHolder.GetComponentsInChildren<SeedSelectable>();
    }

}


public enum PlantType
{
    wheat,
    carrot
}