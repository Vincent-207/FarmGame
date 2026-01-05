using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public Disease[] diseasePrefabs;
    public static PlantManager Instance {get; private set;}
    public Plant currentPlant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }         

        
    }

}

