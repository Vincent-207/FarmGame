using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Seed : TileObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float growthDuration;
    [SerializeField]
    float currentGrowthTime;
    [SerializeField]
    int currentGrowthStage;
    [SerializeField]
    Gradient growthGradient;
    SpriteRenderer seedRenderer;
    [SerializeField]
    Sprite[] growthStates;
    void Start()
    {
        currentGrowthTime = 0;
        seedRenderer = GetComponent<SpriteRenderer>();
        
    }

    Gradient CreateGrowthGradient()
    {
        Gradient output = new Gradient();
        return output;
    }

    // Update is called once per frame
    void Update()
    {
        currentGrowthTime += Time.deltaTime;
        currentGrowthStage = math.min((int) (10 * currentGrowthTime/growthDuration), growthStates.Length - 1);
        // seedRenderer.color = growthGradient.Evaluate(currentGrowthStage/(float) growthStates.Length);
        seedRenderer.sprite = growthStates[currentGrowthStage];
    }
    public bool IsHarvestable()
    {
        if(currentGrowthStage >= growthStates.Length - 1)
        {
            Debug.Log("IsHarvestable");
            return true;
        }
        
        return false;
    }
}
