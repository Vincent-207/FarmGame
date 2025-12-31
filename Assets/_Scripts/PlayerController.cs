using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;


    [SerializeField]
    InputActionReference moveActionReference;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveInput = moveActionReference.action.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

}
