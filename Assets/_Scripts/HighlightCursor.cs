using UnityEngine;

public class HighlightCursor : MonoBehaviour
{
    public bool doHighlight;
    [SerializeField]
    GameObject tilePrefab;
    GameObject tile;
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        tile = Instantiate(tilePrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(doHighlight)
        {
            Vector2Int placePos = gameManager.GetMouseWorldPos();
            tile.transform.position = (Vector2) placePos;
            
        }
    }
}
