using UnityEngine;

public class test : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float speed = 5f ;
    
    [SerializeField] private string droite ;
    [SerializeField] private string gauche ;
    [SerializeField] private float horizontal;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {


        if (Input.GetKey(droite))
        {
           horizontal = 1f;
        
    
    
        }
        else if (Input.GetKey(gauche))
        {
            horizontal = -1f;
        }
        

        
        else {horizontal = 0f;
        }
        
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

}
