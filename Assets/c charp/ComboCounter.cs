using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] TMP_Text ComboText;
    public int CurrentCombo;
    void Start()
    {
        
    }

    public void AddCombo(){
        CurrentCombo +=1;
        ComboText.text = CurrentCombo+" HITS";
    }

    public void ResetCombo(){
        CurrentCombo = 0;
        ComboText.text = CurrentCombo+"";
    }
}
