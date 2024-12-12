using UnityEngine;

public class damage : MonoBehaviour
{
    [SerializeField] private int enemymask;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == enemymask){
            other.gameObject.GetComponent<health>().takedmg(10);
            Debug.Log("ntm");
        }
    }
}