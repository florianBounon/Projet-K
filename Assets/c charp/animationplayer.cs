using UnityEngine;

public class animationplayer : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("y")){
            anim.SetTrigger("attack");

        }
    }
}
