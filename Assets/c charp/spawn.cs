using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI; 

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

    public GameObject ComboCount;

    // Références pour les joueurs instanciés
    private GameObject player1Instance;
    private GameObject player2Instance;

    // Référence au keymanager pour accéder aux contrôles des joueurs
    private keymanager keymanager;

    void Start()
    {
        // Récupérer la référence du keymanager
        keymanager = keymanager.instance;

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
            hitstun hitstunScript = playerInstance.GetComponent<hitstun>();

            // Assigner les touches en fonction du joueur
            if (playerNumber == 1)
            {
                playerScript.droite = keymanager.player1Controls.droite;
                playerScript.gauche = keymanager.player1Controls.gauche;
                playerScript.jump = keymanager.player1Controls.jump;
                playerScript.crouch = keymanager.player1Controls.crouch;
                playerScript.attackkey = keymanager.player1Controls.attackkey;
                playerScript.kickkey = keymanager.player1Controls.kickkey;

                playerScript.projectile = keymanager.player1Controls.projectile;
                playerScript.grabkey = keymanager.player1Controls.grabkey;
                playerScript.parrykey = keymanager.player1Controls.parrykey;
                playerScript.dashkey = keymanager.player1Controls.dashkey;

                healthScript.EnemyPlayerNumber = "Player 2";

                playerScript.ComboCount = ComboCount;

                hitstunScript.Combo = ComboCount;

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
                playerScript.droite = keymanager.player2Controls.droite;
                playerScript.gauche = keymanager.player2Controls.gauche;
                playerScript.jump = keymanager.player2Controls.jump;
                playerScript.crouch = keymanager.player2Controls.crouch;
                playerScript.attackkey = keymanager.player2Controls.attackkey;
                playerScript.kickkey = keymanager.player2Controls.kickkey;

                playerScript.projectile = keymanager.player2Controls.projectile;
                playerScript.grabkey = keymanager.player2Controls.grabkey;
                playerScript.parrykey = keymanager.player2Controls.parrykey;
                playerScript.dashkey = keymanager.player2Controls.dashkey;

                healthScript.EnemyPlayerNumber = "Player 1";

                hitstunScript.Combo = ComboCount;

                playerScript.ComboCount = ComboCount;

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

            
            hitstun player1Scripthitstun = player1Instance.GetComponent<hitstun>();
            hitstun player2Scripthitstun = player2Instance.GetComponent<hitstun>();

            player1Scripthitstun.Enemy = player2Instance;
            player2Scripthitstun.Enemy = player1Instance;
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
                hitboxScript.Combo = ComboCount;
            }

            // Appel récursif pour les sous-enfants
            AssignTagToChildren(child, tag, enemyTag);
        }
    }
}


