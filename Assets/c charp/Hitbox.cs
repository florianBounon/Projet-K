using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private string enemytag;
    [SerializeField] private int deg;
    [SerializeField] private int hitstunframes;
    [SerializeField] private float hitlagsec;
    [SerializeField] private BoxCollider2D hitbox;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == enemytag){
            if (other.transform.root.GetComponent<test>().isbackward){
                other.transform.root.GetComponent<Animator>().SetTrigger("blocking");
            }
            else if (transform.root.GetComponent<test>().hitagain == true){
                GameObject.FindWithTag("MainCamera").GetComponent<Animator>().SetTrigger("shake");
                other.transform.root.GetComponent<Animator>().ResetTrigger("endhitstun");
                transform.root.GetComponent<test>().hitagain = false;
                other.transform.root.GetComponent<health>().takedmg(deg);
                other.transform.root.GetComponent<Animator>().SetTrigger("ishit");
                other.transform.root.GetComponent<hitstun>().basehitstun = hitstunframes;
                other.transform.root.GetComponent<hitstun>().Timer += hitlagsec;
                other.transform.root.GetComponent<health>().healthbar.value = other.transform.root.GetComponent<health>().vie;
                if(transform.root.GetComponent<test>().facingleft){
                    StartCoroutine(knockback(-1200f,other.gameObject));
                }
                else{
                    StartCoroutine(knockback(1200f,other.gameObject));
                }
                StopAllCoroutines();
                StartCoroutine(hitlag(hitlagsec,other.gameObject));
            }
        }
    }
    private IEnumerator hitlag(float duration,GameObject enemy){
        transform.root.GetComponent<Animator>().speed = 0;
        enemy.transform.root.GetComponent<Animator>().speed = 0;
        yield return new WaitForSeconds(duration);
        transform.root.GetComponent<Animator>().speed = 1;
        enemy.transform.root.GetComponent<Animator>().speed = 1;
        enemy.transform.root.GetComponent<hitstun>().hitbyattack = true;
    }

    private IEnumerator knockback(float xknockback,GameObject enemy){
        enemy.transform.root.GetComponent<Rigidbody2D>().AddForce(new Vector2(xknockback, 500f));
        yield return new WaitForSeconds(0.15f);
    }
}