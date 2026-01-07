
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoilSample : EquipableItem
{
    [SerializeField]
    float maxScale;
    public SpriteRenderer soil;
    public SpriteRenderer sample;
    [SerializeField]
    Transform soilWindow;
    
    public void SetSample(float normalizedPos)
    {
        // only sets if higher than current.
        Vector3 scale = sample.transform.localScale;
        float inputWorldDepth = maxScale * normalizedPos;
        scale.y = inputWorldDepth > scale.y ? inputWorldDepth : scale.y;
        sample.transform.localScale = scale;
    }

    void Start()
    {
        // SetSample(0.5f);
        
    }
    internal override void Update()
    {
        if(isEquipped)
        {
            if(IsWithinSoil())
            {
                float normalDepth = GetNormalizedPos();
                SetSample(normalDepth);
            }
        }
        base.Update();
    }
    bool IsWithinSoil()
    {

        if(transform.position.x > (soil.transform.position.x + soil.transform.lossyScale.x/2)) return false;
        if(transform.position.x < (soil.transform.position.x - soil.transform.lossyScale.x/2)) return false;

        return true;
    }
    float GetNormalizedPos()
    {
        Vector3 bottomOfSampler = transform.position + new Vector3(0, -soilWindow.lossyScale.y/2);
        Vector3 topOfSoil = soil.transform.position + new Vector3(0, soil.transform.lossyScale.y/2);
        float samplePos = transform.position.y -soilWindow.lossyScale.y/2;
        float soilPos = soil.transform.position.y + soil.transform.lossyScale.y/2;

        float depth = (samplePos - soilPos);
        // Debug.Log("Distance: " + depth);

        float maxDepth = soilWindow.lossyScale.y;
        float normalDepth = depth/maxDepth;
        // -y is down +y is up. -1 is max normal depth
        normalDepth = Mathf.Clamp(normalDepth, -1, 0);
        Debug.Log("normalDepth: " + normalDepth);
        return -normalDepth;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 bottomOffset = new Vector3(0, -soilWindow.lossyScale.y/2);
        
        // bottom of sample taker
        // Gizmos.DrawSphere(transform.position + bottomOffset, 1);
        // top of soil
        // Gizmos.DrawSphere(soil.transform.position + new Vector3(0, soil.transform.lossyScale.y/2), 1);
    }

}
