
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedPlacer : MonoBehaviour
{
    public int seeds;
    public InputActionReference placeInput;
    public GameObject seedPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        placeInput.action.started += TryPlaceSeed;
    }
    private void OnDisable()
    {
        
        placeInput.action.started -= TryPlaceSeed;
    }
    // Update is called once per frame
    void Update()
    {
        // PlaceSeed();
    }
    public void TryPlaceSeed(InputAction.CallbackContext obj)
    {
        PlaceSeed();
    }
    void HighlightMouse()
    {
        
    }
    Vector2 PositionToGrid(Vector2 position)
    {
        Vector2 output = position;
        output.x = math.round(position.x);
        output.y = math.round(position.y);
        return output;
    }
    public void PlaceSeed()
    {

        // Vector2 mousePos = Vector2.zero;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
        Vector2 placePos = PositionToGrid(mouseWorldPos);
        Instantiate(seedPrefab, placePos, Quaternion.identity);
        
    }
}
