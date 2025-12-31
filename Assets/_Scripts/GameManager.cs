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
    TileObject[,] grid;
    
    void OnEnable()
    {
        placeAction.action.started += PlaceSeed;
    }
    void OnDisable()
    {
        placeAction.action.started -= PlaceSeed;
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
        return grid[position.x, position.y];
    }

    void updateSeedCounter()
    {
        seedCounter.text = String.Format("Seeds: {0}", seeds);
    }
    void Start()
    {
        updateSeedCounter();
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
