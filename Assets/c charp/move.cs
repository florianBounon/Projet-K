using UnityEngine;

public class test : MonoBehaviour {
    [Header("Attributes")]

    [SerializeField] public string droite ;
    [SerializeField] public string gauche ;
    [SerializeField] public string jump ;
    [SerializeField] public string crouch ;
    [SerializeField] public string attackkey;
    [SerializeField] public string kickkey;
    private bool isgrounded;

    [SerializeField] private float speed  ;
    [SerializeField] private float jetforce  ;
    [SerializeField] private float jumpforce  ;
    [SerializeField] private float dashforce;
    [SerializeField] private float dashtime;
    [SerializeField] private float groundcheckradius;
    [SerializeField] private Transform enemyposition;


    private float lastPressTimedroite = -Mathf.Infinity; 
    private float lastPressTimegauche = -Mathf.Infinity; 
    private float lastdash = -Mathf.Infinity; 
    private float doublePressTime = 0.5f;
    private float dash = 0f;
    private float horizontal = 0f;
    private bool isjump = false;
    private bool isdash = false;
    public bool isbackward = false;
    public bool hitagain = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask collisionlayers;
    public bool facingleft = false;
    Animator anim;

    private void Start() {
        anim = GetComponent<Animator> ();
    }
    private void LateUpdate() {
        if (transform.position.x <= enemyposition.position.x){
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingleft = false;
        }
        else{
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingleft = true;
        }
    }
    void Update()
    {

        isgrounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, collisionlayers);

        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "idle"){
            horizontal = 0f;
        }
        else if (Input.GetKey(droite) && isgrounded)
        {
            if (facingleft){
                isbackward = true;
                horizontal = 0.25f;
            }
            else{
                isbackward = false;
                horizontal = 0.8f;
            }
           
        }
        else if (Input.GetKey(gauche) && isgrounded)
        {
            if (!facingleft){
                isbackward = true;
                horizontal = -0.25f;
            }
            else{
                isbackward = false;
                horizontal = -0.8f;
            }
            
        }
        else if (isgrounded)
        {
            horizontal = 0f;
            isbackward = false;
        }
        else{
            isbackward = false;
        }




        if (Input.GetKeyDown(jump) && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle"){
            isjump = true;
        }
        if (Input.GetKey(crouch)){
            anim.SetBool("iscrouching",true);
        }
        if (Input.GetKeyUp(crouch)){
            anim.SetBool("iscrouching",false);
        }

        if (Input.GetKeyDown(attackkey)){
            hitagain = true;
            anim.SetTrigger("attack");
        }
        if (Input.GetKeyDown(kickkey)){
            hitagain = true;
            anim.SetTrigger("kick");
        }
        

        if (Input.GetKeyDown(droite)){
            if (Time.time - lastPressTimedroite <= doublePressTime && dash == 0){
                dash = dashforce;
                lastdash = Time.time;
                isdash = true;
            }
            lastPressTimedroite = Time.time;
        }
        else if (Input.GetKeyDown(gauche)){
            if (Time.time - lastPressTimegauche <= doublePressTime && dash == 0){
                dash = -dashforce;
                lastdash = Time.time;
                isdash = true;
            }
            lastPressTimegauche = Time.time;
        }

        if (Time.time - lastdash >= dashtime) {
            dash=0f;
        }


    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (isjump == true && isgrounded){
            rb.AddForce(new Vector2(0f, jumpforce));
            isjump = false;
        }

        if (isdash == true && isgrounded){
            rb.AddForce(new Vector2(dash ,rb.linearVelocity.y));
            isdash = false;
        }

    }
}
