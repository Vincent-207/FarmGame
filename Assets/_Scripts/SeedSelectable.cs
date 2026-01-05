using UnityEngine;

public class SeedSelectable : MonoBehaviour
{
    public int quantity;
    public GameObject seedPrefab;
    public GameObject plantPrefab;
    public PlantType plantType;
    public void SelectSeed()
    {
        PlantManager plantManager = PlantManager.Instance;
        plantManager.currentSelectedSeed = this;
        GameManager.Instance.seedPlacer.UpdateSelectedSeed();
    }


}

