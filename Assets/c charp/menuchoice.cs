using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assurez-vous que vous avez ce namespace pour TextMesh Pro
using UnityEngine.SceneManagement; // Ajoutez ceci pour charger des scènes
using UnityEngine.InputSystem;

public class ButtonNavigator : MonoBehaviour
{
    public Button[] buttons; // Tableau de boutons (personnages à choisir)

    public RectTransform cube1; // Image du premier joueur (en tant que RectTransform)
    public RectTransform cube2; // Image du deuxième joueur (en tant que RectTransform)
    public float moveSpeed = 5f; // Vitesse de déplacement des images

    public TMP_Text player1NameText; // TMP_Text pour afficher le nom du personnage choisi par le joueur 1
    public TMP_Text player2NameText; // TMP_Text pour afficher le nom du personnage choisi par le joueur 2

    public GameObject[] charactersPrefabs; // Tableau de préfabriqués des personnages (GameObject)
    
    public Transform player1CharacterContainer; // Emplacement où le personnage du joueur 1 apparaîtra
    public Transform player2CharacterContainer; // Emplacement où le personnage du joueur 2 apparaîtra

    private int currentIndex1 = 0; // Indice du bouton sélectionné pour le joueur 1
    private int currentIndex2 = 0; // Indice du bouton sélectionné pour le joueur 2

    private bool isPlayer1Selected = false; // Indique si le joueur 1 a sélectionné un personnage
    private bool isPlayer2Selected = false; // Indique si le joueur 2 a sélectionné un personnage

    private keymanager keyManager;

    private GameObject currentPlayer1Character; // Le GameObject du personnage du joueur 1
    private GameObject currentPlayer2Character; // Le GameObject du personnage du joueur 2

    void Start()
    {
        // Référence au keymanager pour récupérer les entrées configurées
        keyManager = keymanager.instance;

        // Assurer que les cubes commencent à la position du premier bouton.
        if (buttons.Length > 0)
        {
            // Déplacer les cubes au début sur le premier bouton
            UpdateCubePositionAndSize(currentIndex1, cube1);
            UpdateCubePositionAndSize(currentIndex2, cube2);

            // Mettre à jour les noms affichés pour chaque joueur
            UpdateCharacterName(currentIndex1, player1NameText);
            UpdateCharacterName(currentIndex2, player2NameText);

            // Afficher les personnages par défaut
            ShowCharacter(currentIndex1, 1);
            ShowCharacter(currentIndex2, 2);
        }
    }

    void Update()
    {
        // Vérifier les entrées du joueur 1
        if (!isPlayer1Selected)
        {
            if (Input.GetKeyDown(keyManager.player1Controls.droite)) // Déplacement vers la droite
            {
                Navigate(1, ref currentIndex1, cube1);
            }
            else if (Input.GetKeyDown(keyManager.player1Controls.gauche)) // Déplacement vers la gauche
            {
                Navigate(-1, ref currentIndex1, cube1);
            }

            if (Input.GetKeyDown(keyManager.player1Controls.attackkey)) // Attaque pour le joueur 1
            {
                SelectCharacter(currentIndex1, player1NameText, 1);
                isPlayer1Selected = true;

                // Sauvegarder le personnage choisi pour le joueur 1 dans PlayerPrefs
                PlayerPrefs.SetString("Player1Character", buttons[currentIndex1].GetComponentInChildren<TMP_Text>().text);
            }
        }

        // Vérifier les entrées du joueur 2
        if (!isPlayer2Selected)
        {
            if (Input.GetKeyDown(keyManager.player2Controls.droite)) // Déplacement vers la droite
            {
                Navigate(1, ref currentIndex2, cube2);
            }
            else if (Input.GetKeyDown(keyManager.player2Controls.gauche)) // Déplacement vers la gauche
            {
                Navigate(-1, ref currentIndex2, cube2);
            }

            if (Input.GetKeyDown(keyManager.player2Controls.attackkey)) // Attaque pour le joueur 2
            {
                SelectCharacter(currentIndex2, player2NameText, 2);
                isPlayer2Selected = true;

                // Sauvegarder le personnage choisi pour le joueur 2 dans PlayerPrefs
                PlayerPrefs.SetString("Player2Character", buttons[currentIndex2].GetComponentInChildren<TMP_Text>().text);
            }
        }

        // Vérifier si les deux joueurs ont sélectionné leur personnage
        if (isPlayer1Selected && isPlayer2Selected)
        {
            // Charger la scène suivante après les deux sélections
            Debug.Log("Les deux joueurs ont choisi leur personnage !");
            SceneManager.LoadScene("select_map");
        }
    }

    // Fonction pour déplacer la sélection entre les boutons pour chaque joueur
    void Navigate(int direction, ref int currentIndex, RectTransform cube)
    {
        currentIndex += direction;

        // S'assurer que l'indice reste dans les limites du tableau
        if (currentIndex < 0)
            currentIndex = buttons.Length - 1;
        else if (currentIndex >= buttons.Length)
            currentIndex = 0;

        // Mettre à jour la position et la taille du cube du joueur
        UpdateCubePositionAndSize(currentIndex, cube);

        // Mettre à jour le nom du personnage pour chaque joueur
        if (cube == cube1 && !isPlayer1Selected)
            UpdateCharacterName(currentIndex, player1NameText);
        else if (cube == cube2 && !isPlayer2Selected)
            UpdateCharacterName(currentIndex, player2NameText);

        // Afficher le personnage sélectionné pour chaque joueur
        if (cube == cube1)
            ShowCharacter(currentIndex, 1);
        else if (cube == cube2)
            ShowCharacter(currentIndex, 2);
    }

    // Fonction pour mettre à jour la position et la taille du cube (Image UI) en fonction du bouton sélectionné
    void UpdateCubePositionAndSize(int index, RectTransform cube)
    {
        // Obtenir le RectTransform du bouton sélectionné
        RectTransform buttonRectTransform = buttons[index].GetComponent<RectTransform>();

        // Mettre à jour la position de l'image en fonction du bouton
        cube.position = buttonRectTransform.position;

        // Ajuster la taille de l'image pour correspondre à la taille du bouton
        cube.sizeDelta = new Vector2(buttonRectTransform.rect.width, buttonRectTransform.rect.height);
    }

    // Fonction pour sélectionner un personnage et appeler le onClick du bouton
    void SelectCharacter(int index, TMP_Text playerText, int playerNumber)
    {
        // Appeler la méthode onClick du bouton sélectionné
        buttons[index].onClick.Invoke();
        
        // Mettre à jour le texte pour afficher le nom du personnage sélectionné
        UpdateCharacterName(index, playerText);
    }

    // Fonction pour mettre à jour le nom du personnage dans le texte
    void UpdateCharacterName(int index, TMP_Text playerText)
    {
        // Mettre à jour le texte avec le nom du personnage
        playerText.text = buttons[index].GetComponentInChildren<TMP_Text>().text;
    }

    // Fonction pour afficher le personnage sélectionné pour chaque joueur
    void ShowCharacter(int index, int playerNumber)
    {
        GameObject selectedCharacterPrefab = charactersPrefabs[index];

        // Désactiver tous les personnages avant d'activer celui qui est sélectionné
        if (playerNumber == 1)
        {
            // Si un personnage est déjà instancié, le détruire avant d'en instancier un nouveau
            if (currentPlayer1Character != null)
                Destroy(currentPlayer1Character);

            // Instancier le personnage pour le joueur 1 dans son conteneur spécifique
            currentPlayer1Character = Instantiate(selectedCharacterPrefab, player1CharacterContainer.position, Quaternion.identity, player1CharacterContainer);
        }
        else if (playerNumber == 2)
        {
            // Si un personnage est déjà instancié, le détruire avant d'en instancier un nouveau
            if (currentPlayer2Character != null)
                Destroy(currentPlayer2Character);

            // Instancier le personnage pour le joueur 2 dans son conteneur spécifique
            currentPlayer2Character = Instantiate(selectedCharacterPrefab, player2CharacterContainer.position, Quaternion.identity, player2CharacterContainer);
        }
    }
}
