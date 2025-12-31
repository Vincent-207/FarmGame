using UnityEngine;
using UnityEngine.Events;

public class ShopTrigger : MonoBehaviour
{
    public UnityEvent shopEnterEvent, shopExitEvent, shopStayEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        shopEnterEvent.Invoke();
    }
    void OnTriggerExit2D(Collider2D col)
    {
        shopExitEvent.Invoke();
    }
}
