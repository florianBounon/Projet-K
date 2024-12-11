using UnityEngine;

public class test : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float speed  ;
    [SerializeField] private float jetforce  ;
    [SerializeField] private float jet = 0f;
    [SerializeField] private string droite ;
    [SerializeField] private string gauche ;
    [SerializeField] private string jump ;
    [SerializeField] private float horizontal = 0f;
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




        if (Input.GetKey(jump)){
            jet = jetforce;
        }
        else{jet=0;}
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        rb.AddForce(new Vector2(rb.linearVelocity.x, jet));
    }

}
