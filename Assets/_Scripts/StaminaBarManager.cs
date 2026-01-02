using Unity.Collections;
using UnityEngine;

public class StaminaBarManager : MonoBehaviour
{
    public float maxStamina;
    public float currentStamina;
    [SerializeField]
    RectTransform coloredBarRect, whiteBarRect;
    [SerializeField]
    float maxX;
    public void UpdateBar()
    {
        float percentageFull = currentStamina/maxStamina;
        coloredBarRect.sizeDelta = new Vector2(percentageFull * maxX, 50);
        // coloredBarRect.rect = Rect.zero;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coloredBarRect.transform.localPosition = new Vector2(-maxX/4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // coloredBarRect.sizeDelta = bug;
        UpdateBar();
    }
}
