using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Leaf : MonoBehaviour, ISprayable, INutritientable
{
    public Plant plant;
    [SerializeField]
    Sprite[] icons;
    public DiseaseType diseaseType;
    SpriteRenderer spriteRenderer;
    public List<Disease> diseases;
    public GameObject[] diseasePrefabs;
    public double getDiseaseValue()
    {
        double output = 0;
        UpdateDiseases();
        foreach(Disease disease in diseases)
        {
            output += disease.strength;
        }

        return output;
    }
    public void Cure(DiseaseType cureDiseaseType)
    {
        plant.farmingPlant.Cure(cureDiseaseType);
        foreach(Disease disease in diseases)
        {
            if(disease != null && disease.diseaseType == cureDiseaseType)
            {
                Destroy(disease.gameObject);

            }
        }
        diseases.RemoveAll(item => item == null);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        diseases = new List<Disease>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        plant = transform.parent.GetComponent<Plant>();
    }

    public void ApplyNutrients(Spray spray)
    {
        UpdateDiseases();
        Cure(spray.diseaseCureType);
        UpdateDiseases();
    }
    public void ApplySpray(Spray spray)
    {
        // Debug.Log("Apply");
        
    }

    void UpdateDiseases()
    {
        diseases = new List<Disease>();
        transform.GetComponentsInChildren<Disease>(true, diseases);
    }

    public void AddDisease(int diseaseIndex)
    {
        Transform diseaseTransform = Instantiate(PlantManager.Instance.diseasePrefabs[diseaseIndex], Vector3.zero, Quaternion.identity, transform).transform;
        diseaseTransform.localScale = Vector3.one;
        diseaseTransform.localPosition = Vector3.zero;
        Disease newDisease = diseaseTransform.GetComponent<Disease>();
        diseases.Add(newDisease);

        plant.farmingPlant.AddDisease(diseaseIndex);
    }

    
}
