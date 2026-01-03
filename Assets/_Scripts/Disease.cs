using System;
using UnityEngine;
[Serializable]
public class Disease
{
    public String name;
    public DiseaseType diseaseType;
}

public enum DiseaseType
{
    none,
    fungal,
    bacterial,
    terrsitial,
}