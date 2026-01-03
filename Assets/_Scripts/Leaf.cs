using System;
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
    public void Cure()
    {
        spriteRenderer.sprite = icons[0];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
}
