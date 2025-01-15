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
    void Start()
    {
        anim = GetComponent<Animator> ();
    }

    void Update()
    {
        if(hitbyattack == true){
            ishitstun=true;
            hitbyattack=false;
            framesleft = basehitstun-comboscaling;
            comboscaling+=4;
            StopAllCoroutines();
            StartCoroutine(endofhitstun());
        }
        if (ishitstun){
            if (framesleft <= 0){
                ishitstun=false;
                comboscaling=0;
                anim.SetTrigger("endhitstun");
            }
            //StartCoroutine(endofhitstun());
            //Debug.Log(framesleft);
            //framesleft--;
        }
    }

    private IEnumerator endofhitstun(){
        Debug.Log(framesleft/60f);
        yield return new WaitForSeconds(framesleft/60f) ;
        framesleft =0;
    }
}
