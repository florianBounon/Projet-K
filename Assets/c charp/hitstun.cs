using System.Collections;
using UnityEngine;

public class hitstun : MonoBehaviour
{
    Animator anim;
    public int comboscaling;
    public int basehitstun;
    private int framesleft;
    public bool ishitstun = false;
    void Start()
    {
        anim = GetComponent<Animator> ();
    }

    void Update()
    {
        if(anim.GetBool("ishit") == true){
            ishitstun=true;
            framesleft = basehitstun-comboscaling;
            comboscaling+=2; 
        }
        if (ishitstun){
            if (framesleft <= 0){
                ishitstun=false;
                comboscaling=0;
                anim.SetTrigger("endhitstun");
            }
            StartCoroutine(endofhitstun());
        }
    }

    private IEnumerator endofhitstun(){
        yield return new WaitForSeconds(2) ;
        framesleft =0;
    }
}
