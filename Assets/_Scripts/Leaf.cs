using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Leaf : MonoBehaviour, ISprayable
{
    [SerializeField]
    Sprite[] icons;
    SpriteRenderer spriteRenderer;
    public void Cure()
    {
        spriteRenderer.sprite = icons[0];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public void ApplySpray(Spray spray)
    {
        if(spray.diseaseCureType == DiseaseType.fungal)
        {
            Cure();
        }
    }
}
