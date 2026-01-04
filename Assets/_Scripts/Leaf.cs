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
    public void Cure()
    {
        spriteRenderer.sprite = icons[0];
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
        if(spray.diseaseCureType == diseaseType)
        {
            Cure();

        }
    }
    public void ApplySpray(Spray spray)
    {
        Debug.Log("Apply");
        
    }

    public void AddDisease()
    {
        Transform diseaseTransform = Instantiate(PlantManager.Instance.diseasePrefabs[0], Vector3.zero, Quaternion.identity, transform).transform;
        diseaseTransform.localScale = Vector3.one;
        diseaseTransform.localPosition = Vector3.zero;
        Disease newDisease = diseaseTransform.GetComponent<Disease>();
        diseases.Add(newDisease);
    }
}
