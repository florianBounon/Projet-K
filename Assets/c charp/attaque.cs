using UnityEngine;

public class attaque : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    void Update()
    {
        if(Input.GetKey("e")){
            Instantiate(hitbox, transform.position+(transform.forward*2), transform.rotation);
        }
    }
}
