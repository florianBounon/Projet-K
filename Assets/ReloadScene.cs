using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r")){
            //SceneManager.LoadScene("ArenaKK");
            SceneManager.LoadScene("arene1");
        }
    }
}
