using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabSeeds;
    SeedSelectable[] seeds;
    public SeedSelectable[] GetSeeds()
    {
        return GetComponentsInChildren<SeedSelectable>();
    }

    public void AddSeed(GameObject SelectableSeedPrefab)
    {
        seeds = GetSeeds();
        SeedSelectable newSeed = SelectableSeedPrefab.GetComponent<SeedSelectable>();
        foreach(SeedSelectable seedSelectable in seeds)
        {
            if(newSeed == seedSelectable)
            {
                Debug.Log("NO!");

            }
            else
            {
                Debug.Log("YES!");
                seedSelectable.IncrementQuantity();
                return;
            }
        }
        Instantiate(SelectableSeedPrefab, transform);
    }

    public void AddSeed(int prefabIndex)
    {
        AddSeed(prefabSeeds[prefabIndex]);
    }

    public void AddSeed(PlantType plantType)
    {
        AddSeed((int) plantType);
    }
}

