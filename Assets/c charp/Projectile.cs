using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // Vitesse du projectile
    public float lifetime = 3f; // Temps avant destruction
    public Vector2 direction = Vector2.right; // Direction de tir

    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime); // Auto-destruction après un temps défini
    }
    
}