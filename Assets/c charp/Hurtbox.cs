using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private int vie = 100;
    [SerializeField] private string CharaName;
    Animator anim;
    private GameObject epicwin;
    
    void Start()
    {
        epicwin = GameObject.FindWithTag("epicwin");
        anim = transform.parent.GetComponent<Animator> ();
    }

    private void Update() {
        if (anim.GetBool("isdying")==true){
                anim.SetBool("isdying",false);
                anim.SetBool("isdead",true);
            }
    }

    public void takedmg(int degats){
        vie-=degats;

        if (vie==0){
            gameObject.transform.parent.GetComponent<test>().enabled = false;
            gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("deadguy");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("isdying",true);
            epicwin.GetComponent<TMP_Text>().enabled=true;
            epicwin.GetComponent<TMP_Text>().text="Victoire " + CharaName + "<br>" + "Player 1";
        }
    }
}
