using System;
using TMPro;
using UnityEngine;

public class debugHealth : MonoBehaviour
{
    [SerializeField]
    RatLogic ratLogic;
    [SerializeField]
    TMP_Text textBox;
    void Update()
    {
        textBox.text = String.Format("{0}", ratLogic.health);
    }
}
