
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedPlacer : MonoBehaviour
{
    public int seeds;
    public InputActionReference placeInput;
    public GameObject square;
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
        PlaceSeed(obj);
    }
    public void PlaceSeed(InputAction.CallbackContext obj)
    {

        // Vector2 mousePos = Vector2.zero;
        Vector2 placePos = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
        square.transform.position = placePos;
    }
}
