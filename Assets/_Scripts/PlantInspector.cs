using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlantInspector : MonoBehaviour
{
    [SerializeField]
    InputActionReference inspectReference;
    public Plant plant;
    public UnityEvent inspect;
    Image plantImage;
    private void OnEnable()
    {
        inspectReference.action.started += TryInspect;
    }
    private void OnDisable()
    {
        
        inspectReference.action.started -= TryInspect;
    }
    public void TryInspect(InputAction.CallbackContext obj)
    {
        GameManager gameManager = GameManager.Instance;
        Vector2Int gridPos = gameManager.GetMouseGridPos();
        if(gameManager.IsValidTile(gridPos))
        {
            TileObject tile = gameManager.GetTile(gridPos);
            plant = tile.GetComponent<Plant>();
            inspect.Invoke();
        }
    }
}
