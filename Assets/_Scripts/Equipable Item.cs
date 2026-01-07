using UnityEngine;
using UnityEngine.InputSystem;

public class EquipableItem : MonoBehaviour
{
    public InputActionReference drop;
    public bool isEquipped {get; set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    internal virtual void OnEnable()
    {
        drop.action.started += DropItem;
    }

    internal virtual void OnDisable()
    {
        drop.action.started -= DropItem;
        
    }

    internal virtual void DropItem(InputAction.CallbackContext callbackContext)
    {
        isEquipped = false;
    }

    internal virtual void MoveWithMouse()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mouseWorldPos;
    }

    internal virtual void Update()
    {
        if(isEquipped)
        {
            MoveWithMouse();
        }
    }

    internal virtual void OnMouseUpAsButton()
    {
        isEquipped = true;
    }
}
