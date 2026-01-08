using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Sherbert.Framework.Generic;
using UnityEngine.AI;
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
    public SerializableDictionary<DiseaseType, DiseaseInfection> diseaseInfections = new();

    void Start()
    {
        currentGrowthTime = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    void UpdateDiseaseInfections()
    {
        Disease[] allDiseases = GetComponentsInChildren<Disease>();
        int diseaseTypeCount = Enum.GetValues(typeof(DiseaseType)).Length;
        /* // reset all values
        for(int i = 0; i < diseaseTypeCount; i++)
        {
            DiseaseInfection diseaseInfection;
            diseaseInfections.TryGetValue((DiseaseType) i, out diseaseInfection);
            
        } */

        foreach(Disease disease in allDiseases)
        {
            DiseaseInfection diseaseInfection = new DiseaseInfection()
            diseaseInfections.Add(disease.diseaseType);
        }
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

    }

    public void GenerateDiseases()
    {
        Debug.Log("Gening!");
        List<Disease> leafDiseases = leaves[0].diseases; 
        if(leafDiseases == null)
        {
            Debug.Log("is null :{");
        }
        leaves[0].AddDisease();
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

public class DiseaseInfection
{
    public DiseaseType diseaseType;
    public float infectionAmount;
    public DiseaseInfection()
    {
        
    }

    public DiseaseInfection(float infectionAmount, DiseaseType diseaseType)
    {
        this.infectionAmount = infectionAmount;
        this.diseaseType = diseaseType;
    }
}

public interface INutritientable
{
    public void ApplyNutrients(Spray spray);
}
