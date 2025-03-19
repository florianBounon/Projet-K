using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] private string grabkey;
    [SerializeField] private string parrykey;
    [SerializeField] private string dashkey;
    public bool isgrounded;
    

    [SerializeField] private float speed  ;
    [SerializeField] private float jetforce  ;
    [SerializeField] private float jumpforce  ;
    [SerializeField] private float dashforce;
    [SerializeField] private float dashtime;
    [SerializeField] private float groundcheckradius;
    [SerializeField] private Transform enemyposition;
    [SerializeField] private string enemytag;



    private float lastPressTimedroite = -Mathf.Infinity; 
    private float lastPressTimegauche = -Mathf.Infinity; 
    private float lastdash = -Mathf.Infinity; 
    private float doublePressTime = 0.2f;
    private float dash = 0f;
    private float horizontal = 0f;
    private bool isjump = false;
    private bool isdash = false;
    private bool dashable = true;
    public bool isbackward = false;
    public bool hitagain = false;
    public bool doublejumpable = false;
    public bool projectilable = true;
    private bool dashspeeding;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask collisionlayers;
    [SerializeField] private GameObject projectileprefab;
    [SerializeField] private GameObject ComboCount;
    public bool facingleft = false;
    private int rota;
    Animator anim;

    private void Start() {
        anim = GetComponent<Animator> ();
    }
    private void LateUpdate() {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "attack" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "AirKick" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "AirPunch" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Kick"){
            if (transform.position.x <= enemyposition.position.x){
                rota = 0;
                facingleft = false;
            }
            else{
                rota = 180;
                facingleft = true;
            }
        }
        transform.rotation = Quaternion.Euler(0, rota, 0);
    }
    void Update()
    {   
        Debug.Log(anim.speed);
        isgrounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, collisionlayers);
        if(!isdash){
            if (!isgrounded){
                anim.SetBool("IsAir",true);
            }
            else{
                anim.SetBool("IsAir",false);
                doublejumpable = true;
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
                anim.SetTrigger("attack");
            }
            if (Input.GetKeyDown(kickkey)){
                anim.SetTrigger("kick");
            }
            if (Input.GetKeyDown(grabkey)){
                anim.SetTrigger("grab");
            }
            if (Input.GetKeyDown(parrykey)){
                anim.SetTrigger("parry");
                StartCoroutine(parrybuffer());
            }
            if (Input.GetKeyDown(projectile) && projectilable)
            {
                anim.SetTrigger("ProjoK");
                StartCoroutine(projobuffer());
            }

            


            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "block"){
                horizontal = 0f;
                isbackward = true;
            }
            /*else if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump"){
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

            }*/
            else if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Jump" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "idle"){
                horizontal = 0f;
                isbackward = false;
            }

            else if (Input.GetKey(droite))
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
            else if (Input.GetKey(gauche))
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
            else
            {
                horizontal = 0f;
                isbackward = false;
            }



            
            

            if (Input.GetKeyDown(droite)){
                if (dashable && Time.time - lastPressTimedroite <= doublePressTime && (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump")){
                    dash = dashforce;
                    StartCoroutine(dashing());
                }
                lastPressTimedroite = Time.time;
            }
            else if (Input.GetKeyDown(gauche)){
                if (dashable && Time.time - lastPressTimegauche <= doublePressTime && (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump")){
                    dash = -dashforce;
                    StartCoroutine(dashing());
                }
                lastPressTimegauche = Time.time;
            }

            if (dashable &&  Input.GetKeyDown(dashkey) && (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump")){
                if (Input.GetKey(droite)){
                    dash = dashforce;
                    StartCoroutine(dashing());
                }
                else if (Input.GetKey(gauche)){
                    dash = -dashforce;
                    StartCoroutine(dashing());
                }
            }

            

        }
        
    }

    private void FixedUpdate() {
        //!transform.GetComponent<hitstun>().ishitstun
        if (dashspeeding){
            rb.linearVelocity = new Vector2(dash,0);
        }
        else if (isgrounded && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle"){
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
        else if (isgrounded){
            rb.linearVelocityX *= 0.95f;
        }

        if (isjump == true && (isgrounded || doublejumpable) && (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "crouch" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump") && !dashspeeding){
            if (!isgrounded){
                doublejumpable = false;

            }
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

    private IEnumerator parrybuffer(){
        yield return new WaitForSeconds(0.2f);
        anim.ResetTrigger("parry");
    }
    private IEnumerator projobuffer(){
        yield return new WaitForSeconds(0.2f);
        anim.ResetTrigger("ProjoK");
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
        yield return new WaitForSeconds(0.3f);
        dashable = true;
    }

    private IEnumerator projocd(){
        yield return new WaitForSeconds(1);
        projectilable = true;
    }

    private void ThrowK(){
        projectilable = false;
        GameObject ProjectileClone = Instantiate(projectileprefab,GetComponent<Transform>());
        ProjectileClone.GetComponent<Hitbox>().Combo = ComboCount;
        ProjectileClone.GetComponent<Hitbox>().Rooted = gameObject;
        ProjectileClone.GetComponent<Hitbox>().enemytag = enemytag;
        if (facingleft){
            ProjectileClone.GetComponent<Projectile>().direction = Vector2.left;
        }
        else{
            ProjectileClone.GetComponent<Projectile>().direction = Vector2.right;
        }
        ProjectileClone.transform.SetParent(null);
        StartCoroutine(projocd());
    }

}
