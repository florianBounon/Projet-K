using System.Collections;
using UnityEngine;

public class hitstun : MonoBehaviour
{
    Animator anim;
    public int comboscaling;
    public int basehitstun;
    private int framesleft;
    public bool ishitstun = false;
    public bool hitbyattack = false;
    public float Timer;
    void Start()
    {
        anim = GetComponent<Animator> ();
        Timer = 0.0f;
    }

    void Update()
    {
        if(hitbyattack == true){
            ishitstun=true;
            hitbyattack=false;
            framesleft = basehitstun-comboscaling;
            comboscaling+=4;
            Timer = framesleft/60.0f;
            //StopAllCoroutines();
            //StartCoroutine(endofhitstun());
        }
        if (ishitstun){
            Timer -= Time.deltaTime;
            //if (framesleft <= 0){
            if (Timer <= 0.0f){
                comboscaling=0;
                anim.SetTrigger("endhitstun");
                ishitstun=false;
            }
            //StartCoroutine(endofhitstun());
            //Debug.Log(framesleft);
            //framesleft--;
        }
    }

    private IEnumerator endofhitstun(){
        Debug.Log(framesleft/60f);
        yield return new WaitForSeconds(framesleft/60f) ;
        Debug.Log("CHIER");
        framesleft =0;
    }
}
