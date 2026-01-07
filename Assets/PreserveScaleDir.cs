using System.Security.Cryptography;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class PreserveScaleDir : MonoBehaviour
{
    
    void Update()
    {

        Vector3 scale = transform.localScale;
        if(transform.lossyScale.y < 0)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
        }
        transform.localScale = scale;
    }
}
