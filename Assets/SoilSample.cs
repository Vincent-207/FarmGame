
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoilSample : MonoBehaviour
{
    [SerializeField]
    float maxScale;
    public bool isEquiped;
    public SpriteRenderer soil;
    public SpriteRenderer sample;
    public void SetSample(float normalizedPos)
    {
        Vector3 scale = sample.transform.localScale;
        scale.y = maxScale * normalizedPos;
        sample.transform.localScale = scale;
    }

    void Start()
    {
        SetSample(0.5f);
    }
    void Update()
    {
        if(isEquiped)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0;
            transform.parent.position = mousePos;
            if(isInSoil())
            {
                SetSample(getNormalizedDepth());
            }
        }
    }

    float getNormalizedDepth()
    {
        float bottomOfSampler = transform.position.y - transform.lossyScale.y/2;
        float topOfSampler = transform.position.y - transform.lossyScale.y/2;
        float topOfSoil = soil.transform.position.y + transform.lossyScale.y/2;
        float depth = (topOfSoil - bottomOfSampler);
        float normalDepth = depth / (transform.lossyScale.y);
        normalDepth = Mathf.Max(0f, normalDepth);
        String debugMsg = String.Format("Depth: {0}, Normalized: {0}", depth, normalDepth);
        Debug.Log(debugMsg);

        return 0;
    }
    bool isInSoil()
    {
        Transform soilTransform = soil.transform;
        if(transform.position.x > (soil.transform.position.x + soil.transform.lossyScale.x/2))
        {
            Debug.Log("out right!");
            return false;
        }
        if(transform.position.x < (soil.transform.position.x - soil.transform.lossyScale.x/2))
        {
            Debug.Log("out left");
            return false;
        }



        return true;
    }
    void OnMouseUpAsButton()
    {
        isEquiped = true;
    }

}
