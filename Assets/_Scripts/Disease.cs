using System;
using UnityEngine;
[Serializable]
public class Disease : MonoBehaviour
{
    public String intenalName;
    public float strength;
    public DiseaseType diseaseType;
    public Disease(String name, DiseaseType diseaseType)
    {
        this.intenalName = name;
        this.diseaseType = diseaseType;
    }

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
}

public enum DiseaseType
{
    none,
    fungal,
    bacterial,
    terrsitial,
}