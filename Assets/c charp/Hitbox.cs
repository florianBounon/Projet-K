using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private string enemytag;
    [SerializeField] private int deg;
    [SerializeField] private int hitstunframes;
    [SerializeField] private float hitlagsec;
    [SerializeField] private float blocklagsec;
    [SerializeField] private float HorizKB;
    [SerializeField] private float VertiKB;
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private GameObject Combo;

    private void OnTriggerEnter2D(Collider2D other) {
        transform.root.GetComponent<Animator>().SetBool("Gatling",true);
        if (other.gameObject.tag == enemytag){
            if (other.transform.root.GetComponent<test>().isbackward && transform.root.GetComponent<test>().hitagain == true){
                other.transform.root.GetComponent<Animator>().Play("block");
                transform.root.GetComponent<test>().hitagain = false;
                other.transform.root.GetComponent<hitstun>().basehitstun = hitstunframes;
                other.transform.root.GetComponent<hitstun>().Timer += blocklagsec;
                StopAllCoroutines();
                StartCoroutine(hitlag(blocklagsec,other.gameObject));
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
                StartCoroutine(hitlag(hitlagsec,other.gameObject));
                //other.transform.root.GetComponent<hitstun>().hitbyattack = true;
            }
        }
    }
    private IEnumerator hitlag(float duration,GameObject enemy){

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

        if(transform.root.GetComponent<test>().facingleft){
            knockback(-HorizKB,VertiKB,enemy.gameObject);
        }
        else{
            knockback(HorizKB,VertiKB,enemy.gameObject);
        }
        enemy.transform.root.GetComponent<hitstun>().hitbyattack = true;
    }

    private void knockback(float xknockback,float yknockback,GameObject enemy){
        enemy.transform.root.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0,0);
        enemy.transform.root.GetComponent<Rigidbody2D>().AddForce(new Vector2(xknockback, yknockback));
    }
}

