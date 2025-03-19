using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private string enemytag;
    [SerializeField] private string parrytag;
    [SerializeField] private int deg;
    [SerializeField] private int hitstunframes;
    [SerializeField] private int blockstunframes;
    [SerializeField] private float hitlagsec;
    [SerializeField] private float blocklagsec;
    [SerializeField] private float HorizKB;
    [SerializeField] private float VertiKB;
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private GameObject Combo;

    private void OnTriggerEnter2D(Collider2D other) {
        transform.root.GetComponent<Animator>().SetBool("Gatling",true);
        if (other.gameObject.tag == enemytag){
            if (transform.tag == "grab" && other.transform.root.GetComponent<test>().isgrounded){
                other.transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
                other.transform.root.GetComponent<Animator>().SetTrigger("ishit");
                other.transform.root.GetComponent<hitstun>().basehitstun = 120;
                other.transform.root.GetComponent<hitstun>().Timer += hitlagsec;
                StopAllCoroutines();
                StartCoroutine(hitlag(hitlagsec,gameObject, false, false));
                transform.root.GetComponent<Animator>().SetTrigger("kick");
            }
            if (other.transform.root.GetComponent<test>().isbackward && transform.root.GetComponent<test>().hitagain == true){
                other.transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
                other.transform.root.GetComponent<Animator>().Play("block");
                transform.root.GetComponent<test>().hitagain = false;
                other.transform.root.GetComponent<hitstun>().basehitstun = blockstunframes;
                other.transform.root.GetComponent<hitstun>().Timer += blocklagsec;
                StopAllCoroutines();
                StartCoroutine(hitlag(blocklagsec,other.gameObject,true, false));
            }
            else if (transform.root.GetComponent<test>().hitagain || gameObject.tag == "Projectile"){
                Combo.GetComponent<ComboCounter>().AddCombo();
                GameObject.FindWithTag("MainCamera").GetComponent<Animator>().SetTrigger("shake");
                other.transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
                transform.root.GetComponent<test>().hitagain = false;
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
            transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
            transform.root.GetComponent<Animator>().SetTrigger("ishit");
            transform.root.GetComponent<hitstun>().basehitstun = 120;
            transform.root.GetComponent<hitstun>().Timer += hitlagsec;
            StopAllCoroutines();
            StartCoroutine(hitlag(hitlagsec,gameObject, false, true));
        }
    }
    private IEnumerator hitlag(float duration,GameObject enemy, bool blocked, bool parried){

        Vector2 Momentum = transform.root.GetComponent<Rigidbody2D>().linearVelocity;
        transform.root.GetComponent<Animator>().speed = 0;
        enemy.transform.root.GetComponent<Animator>().speed = 0;
        enemy.transform.root.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        transform.root.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;

        yield return new WaitForSeconds(duration);

        enemy.transform.root.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        transform.root.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        transform.root.GetComponent<Animator>().speed = 1;
        enemy.transform.root.GetComponent<Animator>().speed = 1;
        transform.root.GetComponent<Rigidbody2D>().linearVelocity = Momentum;
        if(!parried){
            if(transform.root.GetComponent<test>().facingleft){
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
        }
        else{
            transform.root.GetComponent<hitstun>().hitbyattack = true;
        }
    }

    private void knockback(float xknockback,float yknockback,GameObject enemy){
        enemy.transform.root.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0,0);
        enemy.transform.root.GetComponent<Rigidbody2D>().AddForce(new Vector2(xknockback, yknockback));
    }
}

