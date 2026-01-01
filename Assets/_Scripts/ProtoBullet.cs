using UnityEngine;

public class ProtoBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float moveSpeed = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPosition = (transform.up * moveSpeed * Time.deltaTime) + transform.position;
        transform.position = newPosition;

    }

}
