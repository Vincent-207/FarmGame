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
}
