
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedPlacer : MonoBehaviour
{
    public GameObject seedPrefab;
    public bool TryPlaceSeed()
    {
        // out of bounds
        // already filled
        if(GameManager.Instance.GetTile().isOverrideable)
        PlaceSeed();
        return true;
    }
    bool IsInBounds()
    {
        return false;
    }
    public void PlaceSeed()
    {

        // Vector2 mousePos = Vector2.zero;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
        Vector2 placePos = GridHelper.GetCurrentMouseGridPos();
        Instantiate(seedPrefab, placePos, Quaternion.identity);
        
    }
}
