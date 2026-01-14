using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour, ISprayable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float growthDuration;
    [SerializeField]
    float currentGrowthTime;
    [SerializeField]
    int currentGrowthStage;
    [SerializeField]
    Gradient growthGradient;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] growthStates;
    // Other stuffs
    public Leaf[] leaves;
    public Soil soil;
    public FarmingPlant farmingPlant;
    public double growthValue;
    // public SerializableDictionary<DiseaseType, DiseaseInfection> diseaseInfections = new();


    void Start()
    {
        currentGrowthTime = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
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
    public void DoTick()
    {
        
        UpdateValues();
        doGrowth();
        GenerateDiseases();
    }
    void doGrowth()
    {
        double leavesDiseaseValue = 0;
        foreach(Leaf leaf in leaves)
        {
            leavesDiseaseValue += leaf.getDiseaseValue();
        }
        leavesDiseaseValue /= leaves.Length;
        growthValue += 1 - (leavesDiseaseValue);
    }
    
    public void GenerateDiseases()
    {
        Debug.Log("Gening!");
        List<Disease> leafDiseases = leaves[0].diseases; 
        if(leafDiseases == null)
        {
            Debug.Log("is null :{");
        }
        int diseaseCount = PlantManager.Instance.diseasePrefabs.Length;
        int randomDiseaseIndex = UnityEngine.Random.Range(0, diseaseCount);
        leaves[0].AddDisease(randomDiseaseIndex);
    }
    void UpdateValues()
    {
        // spriteRenderer.sprite = growthStates[currentGrowthStage];
        if(soil.salinity > 5)
        {
            Debug.LogWarning("Doing death!");
        }
    }
    public void ApplySpray(Spray spray)
    {
        foreach(Leaf leaf in leaves)
        {
            leaf.ApplyNutrients(spray);
        }
        soil.ApplyNutrients(spray);

        UpdateValues();
    }
}



public interface INutritientable
{
    public void ApplyNutrients(Spray spray);
}
