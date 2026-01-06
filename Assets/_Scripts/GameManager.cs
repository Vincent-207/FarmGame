using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    
    public UnityEvent dayTick, hourTick;
    public float time;
    public static GameManager Instance {get; private set;}
    public int seeds, cropCount, coins;
    public SeedPlacer seedPlacer;
    public InputActionReference placeAction, harvestAction, examineAction;
    [SerializeField] TMP_Text seedCounter, wheatCounter, coinCounter, clockTextbox;
    public Vector3 hidePlantsPos;
    [SerializeField]
    GameObject visitorScreen;
    [Header("Grid settings")]
    [SerializeField] int gridWidth, gridHeight;
    [SerializeField] Vector2Int GridPos;
    TileObject[,] grid;
    void Update()
    {
        int newIntTime = (int) ((time + Time.deltaTime)/60);
        int IntTime = (int) (time/60);
        bool isNewHour = (newIntTime - IntTime) > 0;
        String debugMsg = String.Format("Time: {0}, New time: {0}, Int Time: {0}, Int New Time: {0}", time, time + Time.deltaTime, IntTime, newIntTime);

        time += Time.deltaTime;
        
        if(isNewHour)
        {
            Debug.Log(debugMsg);
            Debug.Log("Int time: " +  IntTime.ToString());
            Debug.Log("new hour!");
            Debug.Break();
            EndHour();
        }
        updateClock();
    }
    void updateClock()
    {
        clockTextbox.text = FormatTime(time);
    }
    public void EndHour()
    {
        updateClock();
        VisitorManager.Instance.updateTime(time);
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
        if(seedPlacer.selectedSeed == null)
        {
            seedCounter.text = "No seed selected";
        }
        else
        {
            seedCounter.text = seedPlacer.selectedSeed.plantName + String.Format(" Seeds: {0} ", seedPlacer.selectedSeed.quantity);
            
        }
        // wheatCounter.text = String.Format("Wheat: {0}", cropCount);
        coinCounter.text = String.Format("Coins: {0}", coins);
        updateClock();
    }
    void Start()
    {
        UpdateSigns();
        grid = new TileObject[gridWidth, gridHeight];
        dayTick ??= new UnityEvent();
        hourTick ??= new UnityEvent();
        Debug.Log(PlantManager.Instance.name);
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
    public String FormatTime(float timeInSeconds)
    {
        int minutes = (int) timeInSeconds / 60;
        int seconds = (int) timeInSeconds - minutes * 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
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
