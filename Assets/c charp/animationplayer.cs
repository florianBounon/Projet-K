using UnityEngine;

public class animationplayer : MonoBehaviour
{
    Animator anim;
    [SerializeField] private string attackkey;
    void Start()
    {
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackkey)){
            anim.SetTrigger("attack");

        }
    }
}
