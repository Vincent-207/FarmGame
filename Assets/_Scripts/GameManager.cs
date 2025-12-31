using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public int seeds;
    public SeedPlacer seedPlacer;
    public InputActionReference placeAction;
    [SerializeField]
    TMP_Text seedCounter;
    [SerializeField]
    int gridWidth, gridHeight;
    TileObject[,] grid;
    
    void OnEnable()
    {
        placeAction.action.started += PlaceSeed;
    }
    void OnDisable()
    {
        placeAction.action.started -= PlaceSeed;
    }
    public bool IsInBounds(Vector2Int pos)
    {
        if(pos.x < 0 || pos.x >= grid.Length)
        {
            return false;
        }
        else if(pos.y < 0 || pos.y >= grid.Length)
        {
            return false;
        }

        return true;
    }
    public void AddTile(TileObject tileObject, Vector2Int gridLocalPosititon)
    {
        grid[gridLocalPosititon.x, gridLocalPosititon.y] = tileObject;
    }

    public void PlaceSeed(InputAction.CallbackContext obj)
    {
        if(seeds > 0)
        {
            seedPlacer.TryPlaceSeed();
            seeds--;
            
        }
        updateSeedCounter();
    }

    public TileObject GetTile(Vector2Int position)
    {
        if(position.x < 0 || position.x > grid.Length)
        {
            Debug.LogWarning("X is out of bounds");
            return null;
        }
        else if(position.y < 0 || position.y > grid.GetLength(0))
        {
            Debug.LogWarning("Y is out of bounds");
            return null;
        }
        return grid[position.x, position.y];
    }

    void updateSeedCounter()
    {
        seedCounter.text = String.Format("Seeds: {0}", seeds);
    }
    void Start()
    {
        updateSeedCounter();
        grid = new TileObject[gridWidth, gridHeight];
    }
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
}
