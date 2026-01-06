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
    [SerializeField]
    GameObject inspectScreen;
    public Vector3 showPos, hidePos, normalPos;
    private void OnEnable()
    {
        inspectReference.action.started += TryInspect;
    }
    private void OnDisable()
    {
        
        inspectReference.action.started -= TryInspect;
    }
    public void CloseInspect()
    {
        GameManager gameManager = GameManager.Instance;
        inspectScreen.SetActive(false);
        inspectScreen.transform.position = hidePos;
        if(plant) plant.transform.position = hidePos;
        Camera.main.transform.position = normalPos;
    }
    public void TryInspect(InputAction.CallbackContext obj)
    {
        GameManager gameManager = GameManager.Instance;
        Vector2Int gridPos = gameManager.GetMouseGridPos();
        if(gameManager.IsValidTile(gridPos))
        {
            TileObject tile = gameManager.GetTile(gridPos);
            plant = tile.GetComponent<FarmingPlant>().plant;
            PlantManager.Instance.currentPlant = plant;
            ShowInspectScreen(plant);
            inspect.Invoke();
        }
    }

    void ShowInspectScreen(Plant plant)
    {
        // move everything to be shown 
        inspectScreen.SetActive(true);
        inspectScreen.transform.position = showPos;
        plant.transform.position = showPos;
        Vector3 cameraPos = showPos;
        cameraPos.z = -10;
        Camera.main.transform.position = cameraPos;
    }

    void Start()
    {
        normalPos = Camera.main.transform.position;
    }
}
