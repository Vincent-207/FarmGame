
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
        // Check for whether this can place the seed. return false and abort so game manager knows and can handle it. 
        
        plantManager = PlantManager.Instance;
        gameManager = GameManager.Instance;
        Vector2Int placePos = gameManager.GetMouseGridPos();
        TileObject previousTile = gameManager.GetTile(placePos);
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
        if(plantManager.currentSelectedSeed == null || plantManager.currentSelectedSeed.quantity <= 0)
        {
            return false;
        }
        
        UpdateSelectedSeed();
        PlaceSeed(placePos);
        return true;
    }
    public void PlaceSeed(Vector2Int placePos)
    {
        // TODO make dependent on grid position;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
        TileObject placedSeed = Instantiate(seedPrefab,(Vector2) placePos, Quaternion.identity).GetComponent<TileObject>();
        GameManager.Instance.AddTile(placedSeed, placePos);
        
        GameObject inspectionPlant = Instantiate(plantPrefab);
        inspectionPlant.transform.position = gameManager.hidePlantsPos;
        placedSeed.GetComponent<FarmingPlant>().plant = inspectionPlant.GetComponent<Plant>();
        selectedSeed.quantity--;
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
