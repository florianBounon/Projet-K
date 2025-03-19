using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{

    [SerializeField] public string enemytag;
    [SerializeField] public string owntag;
    [SerializeField] private string parrytag;
    [SerializeField] private int deg;
    [SerializeField] private int hitstunframes;
    [SerializeField] private int blockstunframes;
    [SerializeField] private float hitlagsec;
    [SerializeField] private float blocklagsec;
    [SerializeField] private float HorizKB;
    [SerializeField] private float VertiKB;
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] public GameObject Combo;
    [SerializeField] public GameObject Rooted;

    private void OnTriggerEnter2D(Collider2D other) {
        Rooted.GetComponent<Animator>().SetBool("Gatling",true);
        if (other.gameObject.tag == enemytag){
            if (transform.tag == "grab" && other.transform.root.GetComponent<test>().isgrounded){
                other.transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
                other.transform.root.GetComponent<Animator>().SetTrigger("ishit");
                other.transform.root.GetComponent<hitstun>().basehitstun = 120;
                other.transform.root.GetComponent<hitstun>().Timer += hitlagsec;
                StopAllCoroutines();
                StartCoroutine(hitlag(hitlagsec,gameObject, false, false));
                Rooted.GetComponent<Animator>().SetTrigger("kick");
            }
            if (other.transform.root.GetComponent<test>().isbackward && (Rooted.GetComponent<test>().hitagain == true || gameObject.tag == "Projectile")){
                other.transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
                other.transform.root.GetComponent<Animator>().Play("block");
                Rooted.GetComponent<test>().hitagain = false;
                other.transform.root.GetComponent<hitstun>().basehitstun = blockstunframes;
                other.transform.root.GetComponent<hitstun>().Timer += blocklagsec;
                StopAllCoroutines();
                StartCoroutine(hitlag(blocklagsec,other.gameObject,true, false));
            }
            else if (Rooted.GetComponent<test>().hitagain || gameObject.tag == "Projectile"){
                Combo.GetComponent<ComboCounter>().AddCombo();
                GameObject.FindWithTag("MainCamera").GetComponent<Animator>().SetTrigger("shake");
                other.transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
                Rooted.GetComponent<test>().hitagain = false;
                other.transform.root.GetComponent<health>().takedmg(deg);
                other.transform.root.GetComponent<Animator>().SetTrigger("ishit");
                other.transform.root.GetComponent<hitstun>().basehitstun = hitstunframes;
                other.transform.root.GetComponent<hitstun>().Timer += hitlagsec;
                other.transform.root.GetComponent<health>().healthbar.value = other.transform.root.GetComponent<health>().vie;
                StopAllCoroutines();
                StartCoroutine(hitlag(hitlagsec,other.gameObject, false, false));
                //other.transform.root.GetComponent<hitstun>().hitbyattack = true;
            }
        }
        else if (other.gameObject.tag == parrytag){
            other.transform.root.GetComponent<Animator>().SetTrigger("endhitstun");
            other.transform.root.Find("ParryBurst").gameObject.GetComponent<ParticleSystem>().Play();
            if (gameObject.tag != "Projectile"){
                Rooted.GetComponent<hitstun>().basehitstun = 120;
            }
            else{
                var temptag = enemytag;
                enemytag = owntag;
                owntag = temptag;
                if (GetComponent<Projectile>().direction == Vector2.right){
                    GetComponent<Projectile>().direction = Vector2.left;
                }
                else{
                    GetComponent<Projectile>().direction = Vector2.right;
                }
                GetComponent<Projectile>().Deviate();
                Rooted.GetComponent<hitstun>().basehitstun = 0;
                Rooted = other.transform.root.gameObject;
            }
            Rooted.GetComponent<Animator>().ResetTrigger("endhitstun");
            Rooted.GetComponent<Animator>().SetTrigger("ishit");
            Rooted.GetComponent<hitstun>().Timer += hitlagsec;
            StartCoroutine(hitlag(hitlagsec,other.gameObject, false, true));
        }
    }
    private IEnumerator hitlag(float duration,GameObject enemy, bool blocked, bool parried){

        Vector2 Momentum = Rooted.GetComponent<Rigidbody2D>().linearVelocity;
        Rooted.GetComponent<Animator>().speed = 0;
        enemy.transform.root.GetComponent<Animator>().speed = 0;
        enemy.transform.root.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        Rooted.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;

        yield return new WaitForSeconds(duration);

        enemy.transform.root.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        Rooted.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        Rooted.GetComponent<Animator>().speed = 1;
        enemy.transform.root.GetComponent<Animator>().speed = 1;
        Rooted.GetComponent<Rigidbody2D>().linearVelocity = Momentum;
        if(!parried){
            if(Rooted.GetComponent<test>().facingleft){
                knockback(-HorizKB,VertiKB,enemy.gameObject);
                if (blocked){
                    knockback(HorizKB,0,gameObject);
                }
            }
            else{
                knockback(HorizKB,VertiKB,enemy.gameObject);
                if (blocked){
                    knockback(-HorizKB,0,gameObject);
                }
            }
            enemy.transform.root.GetComponent<hitstun>().hitbyattack = true;
            if (gameObject.tag == "Projectile"){
                Destroy(gameObject);
            }
        }
        else{
            Rooted.GetComponent<hitstun>().hitbyattack = true;
            /*if (gameObject.tag == "Projectile"){
                gameObject.GetComponent<Rigidbody2D>.linearVelocityX *=-1;
            }*/
        }
        
    }

    private void knockback(float xknockback,float yknockback,GameObject enemy){
        enemy.transform.root.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0,0);
        enemy.transform.root.GetComponent<Rigidbody2D>().AddForce(new Vector2(xknockback, yknockback));
    }
}

