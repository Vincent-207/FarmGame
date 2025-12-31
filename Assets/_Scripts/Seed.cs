using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Seed : TileObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int growthStages;
    public float growthDuration;
    [SerializeField]
    float currentGrowthTime;
    [SerializeField]
    int currentGrowthStage;
    [SerializeField]
    Gradient growthGradient;
    SpriteRenderer seedRenderer;
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
        currentGrowthStage = (int) (10 * currentGrowthTime/growthDuration);
        seedRenderer.color = growthGradient.Evaluate(currentGrowthStage/(float) growthStages);

    }
    public bool IsHarvestable()
    {
        if(currentGrowthStage >= growthStages)
        {
            Debug.Log("IsHarvestable");
            return true;
        }
        
        return false;
    }
}
