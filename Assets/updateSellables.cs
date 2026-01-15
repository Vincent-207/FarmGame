using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class updateSellables : MonoBehaviour
{
    [SerializeField]
    CropSellable[] cropSellables;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cropSellables = GetComponentsInChildren<CropSellable>();
        DoUpdate();
    }

    public void DoUpdate()
    {
        foreach(CropSellable cropSellable in cropSellables)
        {
            cropSellable.UpdateQuantity();
        }
    }
}
