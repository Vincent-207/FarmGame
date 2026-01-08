using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class medicineBottle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField]
    GameObject closestLeaf, spray;
    [SerializeField]
    Sprite openHandle, closedHandle;
    public Image image;
    [SerializeField]
    bool isEquipped;
    RectTransform rectTransform;
    public float rotationSpeed;
    Rigidbody2D rb2D;
    void Start()
    {
        // image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(isEquipped)
        {
            doEquippedStuff();
        }
    }
    void doEquippedStuff()
    {
        
        // transform.position = (Vector2) GameManager.Instance.GetMouseWorldPos();
        transform.position = Mouse.current.position.ReadValue();
        Vector3 toLeaf = (closestLeaf.transform.position - transform.position);
        Debug.DrawRay(transform.position, toLeaf, Color.red);
        toLeaf.Normalize();
        float rot_z = Mathf.Atan2(toLeaf.y, toLeaf.x) * Mathf.Rad2Deg;
        Quaternion toRotation =  Quaternion.Euler(0f, 0f, rot_z - 180);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // Adjust scale so side with spigot always faces the leaf
        Vector3 scale = transform.localScale;
        scale.y = toLeaf.x < 0 ? 1 : -1;
        transform.localScale = scale;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(isEquipped == false)
        {
            isEquipped = true;
            return;
        }
        else
        {
            
        }

    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        image.sprite = closedHandle;
        spray.SetActive(true);

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        // image.sprite = openHandle;
        image.sprite = openHandle;
        spray.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        /* Debug.Log("Cure!");
        // TODO: check if spray hit right thing;
        Leaf leaf = collider.GetComponent<Leaf>();
        if(leaf != null)
        {
            leaf.Cure();
        } */
    }
}
