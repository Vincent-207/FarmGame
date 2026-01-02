
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedPlacer : MonoBehaviour
{
    public GameObject seedPrefab;
    public bool TryPlaceSeed()
    {
        GameManager gameManager = GameManager.Instance;
        Vector2Int placePos = gameManager.GetMouseGridPos();
        TileObject previousTile = gameManager.GetTile(placePos);
        // out of bounds
        // already fille
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
        
        PlaceSeed(placePos);
        return true;
    }
    public void PlaceSeed(Vector2Int placePos)
    {
        // TODO make dependent on grid position;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
        TileObject placedSeed = Instantiate(seedPrefab,(Vector2) placePos, Quaternion.identity).GetComponent<TileObject>();
        GameManager.Instance.AddTile(placedSeed, placePos);
        
    }
}
