using System.Collections;
using UnityEngine;

public class test : MonoBehaviour {
    [Header("Attributes")]

    [SerializeField] private string droite ;
    [SerializeField] private string gauche ;
    [SerializeField] private string jump ;
    [SerializeField] private string crouch ;
    [SerializeField] private string attackkey;
    [SerializeField] private string kickkey;
    [SerializeField] private string projectile;
    public bool isgrounded;
    

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
    private bool dashable = true;
    public bool isbackward = false;
    public bool hitagain = false;
    private bool dashspeeding;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask collisionlayers;
    [SerializeField] private GameObject projectileprefab;
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
        if(!isdash){
            if (!isgrounded){
                anim.SetBool("IsAir",true);
            }
            else{
                anim.SetBool("IsAir",false);
            }

            if (Input.GetKey(crouch)){
                anim.SetBool("iscrouching",true);
            }
            else {
                anim.SetBool("iscrouching",false);
            }

            if (Input.GetKeyDown(jump)){
                isjump = true;
                StartCoroutine(jumpbuffer());
            }

            if (Input.GetKeyDown(attackkey)){
                //hitagain = true;
                //isbackward = false;
                //anim.SetBool("Gatling",false);
                anim.SetTrigger("attack");
            }
            if (Input.GetKeyDown(kickkey)){
                //hitagain = true;
                //isbackward = false;
                anim.SetTrigger("kick");
            }
            if (Input.GetKeyDown(projectile))
            {
                Instantiate(projectileprefab,GetComponent<Transform>());
            }


            


            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "block"){
                horizontal = 0f;
                isbackward = true;
            }
            else if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "idle"){
                horizontal = 0f;

                if (Input.GetKey(droite)){
                    if (facingleft){
                        isbackward = true;}
                    else{
                        isbackward = false;}
                }

                else if (Input.GetKey(gauche)){
                if (!facingleft){
                    isbackward = true;}
                else{
                    isbackward = false;}
                }

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



            
            

            if (Input.GetKeyDown(droite)){
                if (dashable && Time.time - lastPressTimedroite <= doublePressTime && dash == 0 && (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump")){
                    dash = dashforce;
                    StartCoroutine(dashing());
                }
                lastPressTimedroite = Time.time;
            }
            else if (Input.GetKeyDown(gauche)){
                if (dashable && Time.time - lastPressTimegauche <= doublePressTime && dash == 0 && (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump")){
                    dash = -dashforce;
                    StartCoroutine(dashing());
                }
                lastPressTimegauche = Time.time;
            }

            if (Time.time - lastdash >= dashtime) {
                dash=0f;
            }

        }
    }

    private void FixedUpdate() {
        //!transform.GetComponent<hitstun>().ishitstun
        if (dashspeeding){
            rb.linearVelocity = new Vector2(dash,0);
        }
        else if (isgrounded){
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }

        if (isjump == true && isgrounded && (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "crouch")){
            rb.linearVelocity = new Vector2(horizontal * speed, 0);
            rb.AddForce(new Vector2(0f, jumpforce));
            isjump = false;
        }

        //if (isdash == true && isgrounded){
            //rb.AddForce(new Vector2(dash ,rb.linearVelocity.y));
            //isdash = false;
        //}

    }

    private IEnumerator jumpbuffer(){
        yield return new WaitForSeconds(0.2f);
        isjump = false;
    }

    private IEnumerator dashing(){
        lastdash = Time.time;
        isdash = true;
        dashspeeding = true;
        dashable = false;
        anim.SetBool("Dashing",true);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("Dashing",false);
        isdash = false;
        yield return new WaitForSeconds(0.15f);
        dashspeeding = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.linearVelocity = new Vector2(0,0);
        StartCoroutine(dashcd());
    }

    private IEnumerator dashcd(){
        yield return new WaitForSeconds(0.5f);
        dashable = true;
    }
}
