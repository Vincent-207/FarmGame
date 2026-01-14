using System.Collections.Generic;
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
    List<FarmingPlantDisease> farmingPlantDiseases;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxGrowthStage  = sprites.Length - 1;
        plant.farmingPlant = this;
    }
    public void DoDayTick()
    {
        currentGrowthStage = currentGrowthStage >= maxGrowthStage ? maxGrowthStage : currentGrowthStage + 1;
        spriteRenderer.sprite = sprites[currentGrowthStage];
    }
    public void DoHourTick()
    {
        Debug.Log("Hour!");
        plant.farmingPlant = this;
        if(currentGrowthStage == maxGrowthStage)
        {
            Debug.Log("Plant fully grown, not making more sick.");
            return;
        }
        
        plant.DoTick();
    }
    public void Cure(DiseaseType cureDiseaseType)
    {
        UpdateDiseases();
        foreach(FarmingPlantDisease disease in farmingPlantDiseases)
        {
            if(disease.diseaseType == cureDiseaseType)
            {
                Destroy(disease.gameObject);
            }
        }
    }
    void UpdateDiseases()
    {
        farmingPlantDiseases = new List<FarmingPlantDisease>();
        GetComponentsInChildren<FarmingPlantDisease>(true, farmingPlantDiseases);
    }
    public void AddDisease(int index)
    {
        Instantiate(PlantManager.Instance.diseaseOverlays[index], transform);
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
