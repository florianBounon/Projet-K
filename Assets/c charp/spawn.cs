using UnityEngine;
using UnityEngine.UI; // Ajouter ce using pour manipuler le Slider

public class spawn : MonoBehaviour
{
    // Points d'apparition pour chaque joueur
    public Transform spawnPointPlayer1;
    public Transform spawnPointPlayer2;

    // Préfabriqués des personnages pour les deux joueurs
    public GameObject kman;
    public GameObject test;

    // Sliders pour les joueurs (associé à chaque joueur dans l'inspecteur)
    public Slider healthbarPlayer1;
    public Slider healthbarPlayer2;

    // Références pour les joueurs instanciés
    private GameObject player1Instance;
    private GameObject player2Instance;

    void Start()
    {
        // Récupérer les personnages choisis des joueurs
        string player1Character = PlayerPrefs.GetString("Player1Character", "Default");
        string player2Character = PlayerPrefs.GetString("Player2Character", "Default");

        // Afficher les personnages choisis dans la console (pour le débogage)
        Debug.Log("Joueur 1 a choisi : " + player1Character);
        Debug.Log("Joueur 2 a choisi : " + player2Character);

        // Instancier les deux joueurs sans se soucier de l'ordre
        SpawnPlayer(player1Character, spawnPointPlayer1, 1); // Spawn du joueur 1
        SpawnPlayer(player2Character, spawnPointPlayer2, 2); // Spawn du joueur 2

        // Après instanciation des deux joueurs, assigner les positions des ennemis
        AssignEnemyPositions();
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
            health healthScript = playerInstance.GetComponent<health>();

            // Assigner les touches en fonction du joueur
            if (playerNumber == 1)
            {
                playerScript.droite = "d";  // Par exemple, touche droite pour le joueur 1
                playerScript.gauche = "a";  // Touche gauche pour le joueur 1
                playerScript.jump = "w";    // Touche jump pour le joueur 1
                playerScript.crouch = "s";
                playerScript.attackkey = "e";
                playerScript.kickkey = "f";
                healthScript.EnemyPlayerNumber = "Player 2";

                // Stocker la référence du joueur 1
                player1Instance = playerInstance;

                player1Instance.GetComponent<SpriteRenderer>().sortingOrder = 6;

                // Vérification si le personnage possède le script Health
                healthScript.healthbar = healthbarPlayer1;

                // Assigner le tag et la position de l'ennemi
                AssignTagToAllComponentsRecursively(playerInstance, "Player1", "Player2");
            }
            else if (playerNumber == 2)
            {
                playerScript.droite = ";";  // Touche droite pour le joueur 2
                playerScript.gauche = "k";   // Touche gauche pour le joueur 2
                playerScript.jump = "o";             // Touche jump pour le joueur 2
                playerScript.crouch = "l";
                playerScript.attackkey = "i";
                playerScript.kickkey = "j";
                healthScript.EnemyPlayerNumber = "Player 1";

                // Stocker la référence du joueur 2
                player2Instance = playerInstance;

                // Vérification si le personnage possède le script Health
                healthScript.healthbar = healthbarPlayer2;

                player2Instance.GetComponent<SpriteRenderer>().sortingOrder = 7;

                // Assigner le tag et la position de l'ennemi
                AssignTagToAllComponentsRecursively(playerInstance, "Player2", "Player1");
            }
        }
    }

    // Fonction pour assigner la position de l'ennemi une fois les deux joueurs instanciés
    void AssignEnemyPositions()
    {
        if (player1Instance != null && player2Instance != null)
        {
            // Assigner la position de l'ennemi pour chaque joueur
            test player1Script = player1Instance.GetComponent<test>();
            test player2Script = player2Instance.GetComponent<test>();

            player1Script.enemyposition = player2Instance.transform;  // Joueur 1 connaît la position du joueur 2
            player2Script.enemyposition = player1Instance.transform;  // Joueur 2 connaît la position du joueur 1
        }
    }

    // Fonction récursive pour assigner le tag à tous les composants du playerInstance
    void AssignTagToAllComponentsRecursively(GameObject playerInstance, string tag, string enemyTag)
    {
        // Assigner le tag au GameObject principal
        playerInstance.tag = tag;

        // Récursion pour assigner le tag à tous les enfants et sous-enfants
        AssignTagToChildren(playerInstance.transform, tag, enemyTag);
    }

    // Fonction récursive pour assigner le tag aux enfants et sous-enfants d'un Transform
    void AssignTagToChildren(Transform parentTransform, string tag, string enemyTag)
    {
        // Appliquer le tag à chaque enfant du parent
        foreach (Transform child in parentTransform)
        {
            // Assigner le tag à l'enfant
            child.gameObject.tag = tag;

            // Vérifier si l'objet possède le script Hitbox
            Hitbox hitboxScript = child.GetComponent<Hitbox>();
            if (hitboxScript != null)
            {
                // Si le script Hitbox est trouvé, assigner le tag de l'ennemi
                hitboxScript.enemytag = enemyTag;  // Modifier la variable enemytag avec le tag de l'adversaire
            }

            // Appel récursif pour les sous-enfants
            AssignTagToChildren(child, tag, enemyTag);
        }
    }
}


