using UnityEngine;

public class move : MonoBehaviour
{
    private float speed = 10f;
    private float horizontal ;
    [SerializeField] private Rigidbody2D rb;
    void Update();
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
    }
    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        
    }
}
