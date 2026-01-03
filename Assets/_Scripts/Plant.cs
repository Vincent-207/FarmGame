using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : TileObject, ITickable, ISprayable
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
        currentGrowthStage = currentGrowthStage >= growthStates.Length - 1 ? growthStates.Length - 1: currentGrowthStage + 1;
        UpdateValues();

    }

    void UpdateValues()
    {
        spriteRenderer.sprite = growthStates[currentGrowthStage];
    }

    public void ApplySpray(Spray spray)
    {
        foreach(Leaf leaf in leaves)
        {
            leaf.ApplySpray(spray);
        }
    }
}
