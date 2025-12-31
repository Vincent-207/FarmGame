
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedPlacer : MonoBehaviour
{
    public GameObject seedPrefab;
    public bool TryPlaceSeed(int seedCount)
    {
        PlaceSeed(seedCount);
        return true;
    }
    Vector2 PositionToGrid(Vector2 position)
    {
        Vector2 output = position;
        output.x = math.round(position.x);
        output.y = math.round(position.y);
        return output;
    }
    public void PlaceSeed(int seedCount)
    {

        // Vector2 mousePos = Vector2.zero;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
        Vector2 placePos = PositionToGrid(mouseWorldPos);
        Instantiate(seedPrefab, placePos, Quaternion.identity);
        
    }
}
