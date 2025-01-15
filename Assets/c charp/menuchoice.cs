using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assurez-vous que vous avez ce namespace pour TextMesh Pro
using UnityEngine.SceneManagement; // Ajoutez ceci pour charger des scènes

public class ButtonNavigator : MonoBehaviour
{
    public Button[] buttons; // Tableau de boutons (personnages à choisir)

    public RectTransform cube1; // Image du premier joueur (en tant que RectTransform)
    public RectTransform cube2; // Image du deuxième joueur (en tant que RectTransform)
    public float moveSpeed = 5f; // Vitesse de déplacement des images

    public TMP_Text player1NameText; // TMP_Text pour afficher le nom du personnage choisi par le joueur 1
    public TMP_Text player2NameText; // TMP_Text pour afficher le nom du personnage choisi par le joueur 2

    private int currentIndex1 = 0; // Indice du bouton sélectionné pour le joueur 1
    private int currentIndex2 = 0; // Indice du bouton sélectionné pour le joueur 2

    private bool isPlayer1Selected = false; // Indique si le joueur 1 a sélectionné un personnage
    private bool isPlayer2Selected = false; // Indique si le joueur 2 a sélectionné un personnage

    void Start()
    {
        // Assurer que les cubes commencent à la position du premier bouton.
        if (buttons.Length > 0)
        {
            // Déplacer les cubes au début sur le premier bouton
            UpdateCubePositionAndSize(currentIndex1, cube1);
            UpdateCubePositionAndSize(currentIndex2, cube2);
            // Mettre à jour les noms affichés pour chaque joueur
            UpdateCharacterName(currentIndex1, player1NameText);
            UpdateCharacterName(currentIndex2, player2NameText);
        }
    }

    void Update()
    {
        // Vérifie si l'utilisateur appuie sur les touches directionnelles pour les deux joueurs

        // Mouvement du joueur 1 (Image 1)
        if (Input.GetKeyDown(KeyCode.W) && !isPlayer1Selected) // Exemple de touche pour "monter" pour le joueur 1
        {
            Navigate(1, ref currentIndex1, cube1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isPlayer1Selected) // Exemple de touche pour "descendre" pour le joueur 1
        {
            Navigate(-1, ref currentIndex1, cube1);
        }

        // Mouvement du joueur 2 (Image 2)
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isPlayer2Selected) // Exemple de touche pour "monter" pour le joueur 2
        {
            Navigate(1, ref currentIndex2, cube2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isPlayer2Selected) // Exemple de touche pour "descendre" pour le joueur 2
        {
            Navigate(-1, ref currentIndex2, cube2);
        }

        // Vérifier si un joueur sélectionne son personnage (par exemple, avec la touche "Space" ou "Enter")
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayer1Selected) // Sélection du personnage pour le joueur 1 (Space)
        {
            SelectCharacter(currentIndex1, player1NameText);  // Appeler la fonction de sélection pour le joueur 1
            isPlayer1Selected = true; // Marquer le joueur 1 comme ayant sélectionné un personnage
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlayer2Selected) // Sélection du personnage pour le joueur 2 (Enter)
        {
            SelectCharacter(currentIndex2, player2NameText);  // Appeler la fonction de sélection pour le joueur 2
            isPlayer2Selected = true; // Marquer le joueur 2 comme ayant sélectionné un personnage
        }

        // Vérifier si les deux joueurs ont sélectionné leur personnage
        if (isPlayer1Selected && isPlayer2Selected)
        {
            // Enregistrer les personnages choisis dans PlayerPrefs
            PlayerPrefs.SetString("Player1Character", buttons[currentIndex1].GetComponentInChildren<TMP_Text>().text);
            PlayerPrefs.SetString("Player2Character", buttons[currentIndex2].GetComponentInChildren<TMP_Text>().text);

            // Optionnel : Afficher un message dans la console
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
    void SelectCharacter(int index, TMP_Text playerText)
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

    // La fonction qui sera appelée lorsqu'un personnage est choisi
    public void choicecharacter()
    {
        // Logique pour choisir le personnage
        Debug.Log("Personnage choisi !");
    }
}
