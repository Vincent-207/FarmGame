using UnityEngine;

public class ProtoBullet : MonoBehaviour, IProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float moveSpeed, damage;
    [SerializeField]
    Rigidbody2D rb2D;
    void Start()
    {
        
    }

    void Update()
    {
        rb2D.linearVelocity = transform.up * moveSpeed;

    }

    public float GetDamage()
    {
        return damage;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}

public interface IProjectile
{
    public float GetDamage();
    public void Destroy();
}
