using UnityEngine;
using UnityEngine.Animations;

public class Stem : MonoBehaviour, ISprayable
{
    Plant plant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plant = transform.parent.GetComponent<Plant>();

    }

    public void ApplySpray(Spray spray)
    {
        plant.ApplySpray(spray);
    }
}
