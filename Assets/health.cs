using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] public int vie = 100;
    public void takedmg(int degats){
        vie-=degats;
        if (vie==0){
            Destroy(gameObject);
        }
    }
}
