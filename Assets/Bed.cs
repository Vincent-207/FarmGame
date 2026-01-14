using UnityEngine;

public class Bed : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            GameManager.Instance.EndDay();
        }
    }
}
