using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlantInspector : MonoBehaviour
{
    [SerializeField]
    InputActionReference inspectReference;
    public Seed seed;
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
            seed = tile.GetComponent<Seed>();
            inspect.Invoke();
        }
    }
}
