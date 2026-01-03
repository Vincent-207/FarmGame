using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Leaf : MonoBehaviour
{
    [SerializeField]
    Sprite[] icons;
    String Disease;
    Image image;
    void UpdateIcon()
    {
        if(Disease == "Powdery mildew")
        {
            
        }

    }

    public void Cure()
    {
        image.sprite = icons[0];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
