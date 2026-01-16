
using System.Net.NetworkInformation;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedPlacer : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject plantPrefab;
    public GameManager gameManager;
    public SeedSelectable selectedSeed;
    PlantManager plantManager;
    public bool TryPlaceSeed()
    {
        Debug.Log("Trying to place...");
        // Check for whether this can place the seed. return false and abort so game manager knows and can handle it. 
        
        plantManager = PlantManager.Instance;
        gameManager = GameManager.Instance;
        Vector2Int placePos = gameManager.GetMouseGridPos();
        TileObject previousTile = gameManager.GetTile(placePos);
        Debug.Log("Place pos (local): " + placePos);
        if(gameManager.IsInBounds(placePos) == false)
        {
            Debug.LogWarning("Tile is out of bounds");
            return false;
        }
        else if(previousTile != null && previousTile.isOverrideable == false)
        {
            Debug.LogWarning("Tile is full");
            return false;
        }

        // verify selected seed exists and is valid
        if(plantManager.currentSelectedSeed == null)
        {
            Debug.Log("Seed not found in plant manager");
            return false;
        }
        if(plantManager.currentSelectedSeed.GetQuantity() <= 0)
        {
            Debug.Log("Not enough quantity to plant");
            return false;
        }
        Debug.Log("Placing!");
        PlaceSeed(placePos);
        UpdateSelectedSeed();
        return true;
    }
    public void PlaceSeed(Vector2Int gridPlacePos)
    {
        // TODO make dependent on grid position;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
        Vector2 placeWorldPos = GameManager.Instance.GetMouseWorldPos();
        TileObject placedSeed = Instantiate(seedPrefab,(Vector2) placeWorldPos, Quaternion.identity).GetComponent<TileObject>();
        GameManager.Instance.AddTile(placedSeed, gridPlacePos);
        
        GameObject inspectionPlant = Instantiate(plantPrefab);
        inspectionPlant.transform.position = gameManager.hidePlantsPos;
        placedSeed.GetComponent<FarmingPlant>().plant = inspectionPlant.GetComponent<Plant>();
        selectedSeed.DecrementQuantity();
    }
    public void UpdateSelectedSeed()
    {
        plantManager = PlantManager.Instance;
        selectedSeed = plantManager.currentSelectedSeed;
        seedPrefab = selectedSeed.seedPrefab;
        plantPrefab = selectedSeed.plantPrefab;
        
        gameManager.UpdateSigns();
    }
    void Start()
    {
        gameManager = GameManager.Instance;
    }
}
