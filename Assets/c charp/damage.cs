using UnityEngine;

public class damage : MonoBehaviour
{

    [SerializeField] private string enemytag;
    [SerializeField] private int deg;
    [SerializeField] private BoxCollider2D hitbox;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(gameObject.tag);
        if (other.gameObject.tag == enemytag){
            other.gameObject.GetComponent<health>().takedmg(deg);
        }
    }
}