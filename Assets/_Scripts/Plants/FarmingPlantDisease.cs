using UnityEngine;

public class FarmingPlantDisease : MonoBehaviour
{
    public string diseaseName;
    public DiseaseType diseaseType;
    public float strength;

    public void Init(string diseaseName, DiseaseType diseaseType, float strength)
    {
        this.diseaseName = diseaseName;
        this.diseaseType = diseaseType;
        this.strength = strength;
    }
}
