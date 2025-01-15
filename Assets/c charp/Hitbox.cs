using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private string enemytag;
    [SerializeField] private int deg;
    [SerializeField] private int hitstunframes;
    [SerializeField] private BoxCollider2D hitbox;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == enemytag){
            if (other.transform.root.GetComponent<test>().isbackward){
                other.transform.root.GetComponent<Animator>().SetTrigger("blocking");
            }
            else{
                other.transform.root.GetComponent<health>().takedmg(deg);
                other.transform.root.GetComponent<Animator>().SetTrigger("ishit");
                other.transform.root.GetComponent<hitstun>().basehitstun = hitstunframes;
                other.transform.root.GetComponent<hitstun>().hitbyattack = true;
                other.transform.root.GetComponent<health>().healthbar.value = other.transform.root.GetComponent<health>().vie;
            }
        }
    }
}