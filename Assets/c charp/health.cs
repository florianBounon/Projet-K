using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [SerializeField] private int vie = 100;
    [SerializeField] private string CharaName;
    [SerializeField] private string EnemyPlayerNumber;
    Animator anim;
    private GameObject epicwin;
    
    void Start()
    {
        epicwin = GameObject.FindWithTag("epicwin");
        anim = GetComponent<Animator> ();
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
            GetComponent<test>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("deadguy");
            transform.Find("Hurtboxes").gameObject.SetActive(false);
            anim.SetBool("isdying",true);
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0,0);;
            epicwin.GetComponent<TMP_Text>().enabled=true;
            epicwin.GetComponent<TMP_Text>().text="Victoire " + CharaName + "<br>" + EnemyPlayerNumber;
        }
    }
}
