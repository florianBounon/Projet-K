using UnityEngine;

public class test : MonoBehaviour {
    [Header("Attributes")]

    [SerializeField] private string droite ;
    [SerializeField] private string gauche ;
    [SerializeField] private string jump ;

    [SerializeField] private float speed  ;
    [SerializeField] private float jetforce  ;
    [SerializeField] private float dashforce;
    [SerializeField] private float dashtime;

    private float lastPressTimedroite = -Mathf.Infinity; 
    private float lastPressTimegauche = -Mathf.Infinity; 
    private float lastdash = -Mathf.Infinity; 
    private float doublePressTime = 0.2f;
    private float jet = 0f;
    private float dash = 0f;
    private float horizontal = 0f;


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

        if (Input.GetKeyDown(droite)){
            if (Time.time - lastPressTimedroite <= doublePressTime && dash == 0){
                dash = dashforce;
                lastdash = Time.time;
            }
            lastPressTimedroite = Time.time;
        }
        else if (Input.GetKeyDown(gauche)){
            if (Time.time - lastPressTimegauche <= doublePressTime && dash == 0){
                dash = -dashforce;
                lastdash = Time.time;
            }
            lastPressTimegauche = Time.time;
        }

        if (Time.time - lastdash >= dashtime) {
            dash=0f;
        }


    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        rb.AddForce(new Vector2(rb.linearVelocity.x, jet));

        rb.AddForce(new Vector2(dash ,rb.linearVelocity.y));
    }

}
