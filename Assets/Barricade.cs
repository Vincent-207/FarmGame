using UnityEngine;

public class Barricade : MonoBehaviour
{
    public float health;
    public void DoDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
