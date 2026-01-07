using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FarmingPlant : TileObject, IDayTickable, IHourTickable
{
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    public Plant plant;
    int maxGrowthStage, currentGrowthStage;
    public CropType cropType;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxGrowthStage  = sprites.Length - 1;
    }
    public void DoDayTick()
    {
        currentGrowthStage = currentGrowthStage >= maxGrowthStage ? maxGrowthStage : currentGrowthStage + 1;
        spriteRenderer.sprite = sprites[currentGrowthStage];
    }
    public void DoHourTick()
    {
        Debug.Log("Hour!");
        plant.GenerateDiseases();
    }

    public bool IsHarvestable()
    {
        if(currentGrowthStage >= maxGrowthStage)
        {
            Debug.Log("IsHarvestable");
            return true;
        }
        
        return false;
    }
}
