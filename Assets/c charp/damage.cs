using UnityEngine;

public class damage : MonoBehaviour
{

    [SerializeField] private string enemytag;
    [SerializeField] private int deg;
    [SerializeField] private BoxCollider2D hitbox;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == enemytag){
            if (other.transform.parent.gameObject.GetComponent<test>().isbackward){
                other.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("blocking");
            }
            else{
                other.gameObject.GetComponent<health>().takedmg(deg);
                other.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("ishit");
            }
        }
    }
}