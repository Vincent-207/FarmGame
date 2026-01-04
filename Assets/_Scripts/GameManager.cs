using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public UnityEvent dayTick, hourTick;
    public static GameManager Instance {get; private set;}
    public int seeds, cropCount, coins;
    public SeedPlacer seedPlacer;
    public InputActionReference placeAction, harvestAction, examineAction;
    [SerializeField]
    TMP_Text seedCounter, wheatCounter, coinCounter;
    [SerializeField]
    int gridWidth, gridHeight;
    [SerializeField]
    Vector2Int GridPos;
    TileObject[,] grid;
    public Vector3 hidePlantsPos;
    public void EndHour()
    {
        hourTick.Invoke();
    }
    public Vector2Int GetMouseGridPos()
    {
       
        Vector2Int localMousePosition = GetMouseWorldPos() - GridPos;
        return localMousePosition;

    }
    public Vector2Int GetMouseWorldPos()
    {
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return Vector2Int.RoundToInt(worldMousePosition);
    }
    void Harvest(InputAction.CallbackContext obj)
    {
        Vector2Int gridPos = GridHelper.GetCurrentMouseGridPos();
        TileObject tile = GetTile(gridPos);
        if(tile == null)
        {
            Debug.LogWarning("Couldn't find tile");
            return;
        }

        Plant seed = tile.GetComponent<Plant>();
        if(seed == null)
        {
            Debug.LogWarning("Couldn't find seed");
            return;
        }
        else if(seed.IsHarvestable() == false)
        {
            Debug.LogWarning("Seed isn't harvestable");
            return;
            
        }
        else
        {
            Debug.Log("Harvested!");
            Destroy(grid[gridPos.x, gridPos.y].gameObject);
            cropCount++;
            UpdateSigns();
        }

    }
    void OnEnable()
    {
        placeAction.action.started += PlaceSeed;
        harvestAction.action.started += Harvest;
    }
    void OnDisable()
    {
        placeAction.action.started -= PlaceSeed;
        harvestAction.action.started -= Harvest;
    }
    public bool IsInBounds(Vector2Int pos)
    {
        if(pos.x < 0 || pos.x >= gridWidth)
        {
            return false;
        }
        else if(pos.y < 0 || pos.y >= gridHeight)
        {
            return false;
        }

        return true;
    }
    public void EndDay()
    {
        dayTick.Invoke();
    }
    public bool IsValidTile(Vector2Int gridPos)
    {
        TileObject tile = GetTile(gridPos);

        return IsInBounds(gridPos) && tile != null;
    }
    public void AddTile(TileObject tileObject, Vector2Int gridLocalPosititon)
    {
        grid[gridLocalPosititon.x, gridLocalPosititon.y] = tileObject;
        // Debug.Log("Added tile!");
        // Add day tick
        IDayTickable tickable = tileObject.GetComponent<IDayTickable>();
        if(tickable != null)
        {
            // Debug.Log("Found tickable!");
            dayTick.AddListener(tickable.DoDayTick);
        }
        IHourTickable hourTickable = tileObject.GetComponent<IHourTickable>();
        
        if(hourTickable != null)
        {
            hourTick.AddListener(hourTickable.DoHourTick);
        }
    }

    public void PlaceSeed(InputAction.CallbackContext obj)
    {
        if(seeds > 0)
        {
            if(seedPlacer.TryPlaceSeed())
            {
                seeds--;
                
            }
            
        }
        UpdateSigns();
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
    public void UpdateSigns()
    {
        seedCounter.text = String.Format("Seeds: {0}", seeds);
        wheatCounter.text = String.Format("Wheat: {0}", cropCount);
        coinCounter.text = String.Format("Coins: {0}", coins);
    }
    void Start()
    {
        UpdateSigns();
        grid = new TileObject[gridWidth, gridHeight];
        dayTick ??= new UnityEvent();
        hourTick ??= new UnityEvent();
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

public interface IDayTickable
{
    // Interface for everything that updates every day.
    public void DoDayTick();
}

public interface IHourTickable
{
    public void DoHourTick();
}
