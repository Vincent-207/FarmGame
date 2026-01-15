using UnityEngine;
using UnityEngine.InputSystem;

public class HighlightCursor : MonoBehaviour
{
    public bool doHighlight;
    [SerializeField]
    GameObject tilePrefab;
    GameObject tile;
    GameManager gameManager;
    [SerializeField]
    Vector2 gridOffset;
    public bool isOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        tile = Instantiate(tilePrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("cursor pos: " + gameManager.GetMouseGridPos().ToString());
        doHighlight = gameManager.IsInBounds(gameManager.GetMouseGridPos());
        if(doHighlight)
        {
            tile.SetActive(true);
            Vector2 placePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if(isOffset)
            {
                placePos.x = (int) placePos.x;
                placePos.y = (int) placePos.y;
            }
            else
            {
                placePos = Vector2Int.RoundToInt(placePos);
            }
            tile.transform.position = (Vector2) placePos + gridOffset;
            
        }
        else
        {
            tile.SetActive(false);
        }
    }
}
