using UnityEngine;
using UnityEngine.SceneManagement;

public class selectmap : MonoBehaviour
{
    public void map(string mape)
    {
        // Récupérer les personnages choisis à partir de PlayerPrefs
        string player1Character = PlayerPrefs.GetString("Player1Character", "Default");
        string player2Character = PlayerPrefs.GetString("Player2Character", "Default");

        // Afficher les personnages dans la console pour tester
        Debug.Log("Joueur 1 a choisi : " + player1Character);
        Debug.Log("Joueur 2 a choisi : " + player2Character);

        SceneManager.LoadScene(mape); 
    }
}
