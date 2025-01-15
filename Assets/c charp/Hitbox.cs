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
                transform.root.GetComponent<test>().hitagain = false;
                other.transform.root.GetComponent<health>().takedmg(deg);
                other.transform.root.GetComponent<Animator>().SetTrigger("ishit");
                other.transform.root.GetComponent<hitstun>().basehitstun = hitstunframes;
                other.transform.root.GetComponent<health>().healthbar.value = other.transform.root.GetComponent<health>().vie;
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
}