using UnityEngine;

public class Soil : MonoBehaviour, INutritientable, ISprayable
{
    public Plant plant;
    public float salinity;
    public float ph;
    public void ApplySpray(Spray spray)
    {
        plant.ApplySpray(spray);
    }

    public void ApplyNutrients(Spray spray)
    {
        salinity += spray.salinityIncremenet;
    }

    void Start()
    {
        plant = transform.parent.GetComponent<Plant>();
    }

}
