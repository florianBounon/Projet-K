using UnityEngine;

public class spawn : MonoBehaviour
{
    // Points d'apparition pour chaque joueur
    public Transform spawnPointPlayer1;
    public Transform spawnPointPlayer2;

    // Préfabriqués des personnages pour les deux joueurs
    public GameObject kman;
    public GameObject test;

    void Start()
    {
        // Récupérer les personnages choisis des joueurs
        string player1Character = PlayerPrefs.GetString("Player1Character", "Default");
        string player2Character = PlayerPrefs.GetString("Player2Character", "Default");

        // Afficher les personnages choisis dans la console (pour le débogage)
        Debug.Log("Joueur 1 a choisi : " + player1Character);
        Debug.Log("Joueur 2 a choisi : " + player2Character);

        // Appeler la fonction pour faire apparaître les personnages
        SpawnPlayer(player1Character, spawnPointPlayer1, 1); // Spawn du joueur 1
        SpawnPlayer(player2Character, spawnPointPlayer2, 2); // Spawn du joueur 2
    }

    // Fonction pour faire apparaître les joueurs aux points d'apparition
    void SpawnPlayer(string character, Transform spawnPoint, int playerNumber)
    {
        // Choisir le préfabriqué du personnage
        GameObject characterPrefab = null;

        if (character == "kman")
        {
            characterPrefab = kman;
        }
        else if (character == "test")
        {
            characterPrefab = test;
        }

        if (characterPrefab != null)
        {
            // Instancier le personnage
            GameObject playerInstance = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);

            // Récupérer le script "test" attaché au personnage
            test playerScript = playerInstance.GetComponent<test>();
            animationplayer playerScriptatt = playerInstance.GetComponent<animationplayer>();

            // Assigner les touches en fonction du joueur
            if (playerNumber == 1)
            {
                playerScript.droite = "d";  // Par exemple, touche droite pour le joueur 1
                playerScript.gauche = "a";  // Touche gauche pour le joueur 1
                playerScript.jump = "w";    // Touche jump pour le joueur 1
                playerScriptatt.attackkey = "e";
            }
            else if (playerNumber == 2)
            {
                playerScript.droite = ";";  // Touche droite pour le joueur 2
                playerScript.gauche = "k";   // Touche gauche pour le joueur 2
                playerScript.jump = "o";             // Touche jump pour le joueur 2
                playerScriptatt.attackkey = "i";
            }

            // Assigner le tag spécifique pour chaque joueur
            if (playerNumber == 1)
            {
                playerInstance.tag = "Player1";
            }
            else
            {
                playerInstance.tag = "Player2";
            }
        }
    }
}
