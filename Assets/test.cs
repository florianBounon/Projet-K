using UnityEngine;

public class test : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float speed = 5f ;
    [SerializeField] private float horizontal;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

}
