using UnityEngine;

public class damage : MonoBehaviour
{

    [SerializeField] private string enemytag;
    [SerializeField] private int deg;
    [SerializeField] private BoxCollider2D hitbox;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == enemytag){
            if (other.gameObject.GetComponent<health>()){
            other.gameObject.GetComponent<health>().takedmg(deg);
            }else{
                other.gameObject.GetComponent<PunchingBall>().takedmg(deg);
            }
        }
    }
}