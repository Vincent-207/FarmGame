
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedPlacer : MonoBehaviour
{
    public int seeds;
    public InputActionReference placeInput;
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
        
    }
    public void TryPlaceSeed(InputAction.CallbackContext obj)
    {
        PlaceSeed();
    }
    public void PlaceSeed()
    {

        // Vector2 mousePos = Vector2.zero;
    }
}
