using System;
using System.Data;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SprayBottle : MonoBehaviour 
{
    public Spray spray;
    public Plant plant;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openHandle, closedHandle;
    public bool isEquipped;
    Leaf closestLeaf;
    public float rotationSpeed;
    Rigidbody2D RB;
    public InputActionReference drop;
    bool isEquipClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        drop.action.started += DeEquip;
    }

    void OnDisable()
    {
        drop.action.started -= DeEquip;
        
    }
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();    
    }

    public void DeEquip(InputAction.CallbackContext callbackContext)
    {
        isEquipped = false;
        isEquipClick = false;
        RB.linearVelocity = Vector2.zero;
        RB.angularVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEquipped)
        {
            MoveWithMouse();

        }
    }

    void MoveWithMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0;
        RB.MovePosition(mouseWorldPos);
        
        plant = PlantManager.Instance.currentPlant;
        LookAtClosestLeaf();
        
    }
    
    void LookAtClosestLeaf()
    {
        // update closest leaf
        foreach(Leaf leaf in plant.leaves)
        {
            if(closestLeaf == null)
            {
                closestLeaf = leaf;
            }
            else
            {
                float distanceToLeaf = (leaf.transform.position - transform.position).magnitude;
                float distanceToClosestLeaf = (closestLeaf.transform.position - transform.position).magnitude;
                if(distanceToClosestLeaf < distanceToLeaf)
                {
                    closestLeaf = leaf;
                }
            }
        }


        // rotate to look at it
        Vector3 toLeaf = (closestLeaf.transform.position - transform.position).normalized;
        float lookAngle = Mathf.Atan2(toLeaf.y, toLeaf.x) * Mathf.Rad2Deg;
        Quaternion rotation =  Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, lookAngle - 180), rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;
        // mirror if neccessary, so spigot is always facing the leaf
        
        Vector3 scale = transform.localScale;
        // Mathf.Abs(scale.x)
        scale.y = toLeaf.x < 0 ? 1 : -1;
        transform.localScale = scale;
        
    }

    void OnMouseDown()
    {
        if(isEquipped && !isEquipClick)
        {
            spriteRenderer.sprite = closedHandle;
            
        }
    }

    void OnMouseUp()
    {        
        if(isEquipped)
        {
            isEquipClick = false;
            spriteRenderer.sprite = openHandle;
            
        }
    }

    void OnMouseUpAsButton()
    {
        isEquipped = true;
        isEquipClick = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("Trigger!");
        ISprayable sprayable = collider.GetComponent<ISprayable>();
        if(sprayable != null)
        {
            // Debug.Log("Spray!");
            sprayable.ApplySpray(spray);
        }
        else
        {
            // Debug.Log("not sprayable");
        }
        
    }
}
[Serializable]
public class Spray
{

    float phIncrement;
    public float salinityIncremenet;
    public DiseaseType diseaseCureType;
    
}

public interface ISprayable
{
    public void ApplySpray(Spray spray);
}