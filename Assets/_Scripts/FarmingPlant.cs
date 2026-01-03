using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FarmingPlant : TileObject, ITickable
{
    SpriteRenderer spriteRenderer;
    int currentGrowthStage;
    Sprite[] sprites;
    int maxGrowthStage;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentGrowthStage = maxGrowthStage  = sprites.Length - 1;
    }
    public void DoTick()
    {
        currentGrowthStage++;
        spriteRenderer.sprite = sprites[currentGrowthStage];
    }
}
