using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int seeds;
    public SeedPlacer seedPlacer;
    public InputActionReference placeAction;
    [SerializeField]
    TMP_Text seedCounter;
    void OnEnable()
    {
        placeAction.action.started += PlaceSeed;
    }
    void OnDisable()
    {
        placeAction.action.started -= PlaceSeed;
        
    }

    public void PlaceSeed(InputAction.CallbackContext obj)
    {
        seedPlacer.TryPlaceSeed(seeds);
        seeds--;
    }

    void updateSeedCounter()
    {
        seedCounter.text = String.Format("Seeds: {0}", seeds);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
