using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class selectmap : MonoBehaviour
{
    public Button[] mapButtons; // Tableau de boutons représentant les cartes à sélectionner
    public RectTransform cube; // Cube représentant la sélection de la carte
    public Image mapImageDisplay; // Image UI qui affiche l'image de la carte sélectionnée
    public float moveSpeed = 5f; // Vitesse de déplacement du cube pour la sélection de la carte

    private int currentIndex = 0; // Indice de la carte sélectionnée
    private keymanager keyManager;

    private void Start()
    {
        // Référence au keymanager pour récupérer les entrées configurées
        keyManager = keymanager.instance;

        // Initialisation de la position du cube sur la première carte
        UpdateCubePositionAndImage(currentIndex);
    }

    private void Update()
    {
        // Vérifier les entrées du joueur 1 pour déplacer le cube à gauche ou à droite
        if (Input.GetKeyDown(keyManager.player1Controls.droite)) // Déplacement vers la droite
        {
            Navigate(1); // Déplacer le cube à droite
        }
        else if (Input.GetKeyDown(keyManager.player1Controls.gauche)) // Déplacement vers la gauche
        {
            Navigate(-1); // Déplacer le cube à gauche
        }

        // Vérifier si le joueur 1 appuie sur une touche pour sélectionner la carte
        if (Input.GetKeyDown(keyManager.player1Controls.attackkey)) // Sélectionner la carte
        {
            SelectMap();
        }
    }

    // Fonction pour déplacer la sélection de la carte (le cube)
    private void Navigate(int direction)
    {
        currentIndex += direction;

        // S'assurer que l'indice reste dans les limites du tableau
        if (currentIndex < 0)
            currentIndex = mapButtons.Length - 1;
        else if (currentIndex >= mapButtons.Length)
            currentIndex = 0;

        // Mettre à jour la position du cube et l'image de la carte sélectionnée
        UpdateCubePositionAndImage(currentIndex);
    }

    // Fonction pour mettre à jour la position du cube et l'image de la carte
    private void UpdateCubePositionAndImage(int index)
    {
        // Mettre à jour la position du cube sur le bouton sélectionné
        RectTransform buttonRectTransform = mapButtons[index].GetComponent<RectTransform>();
        cube.position = buttonRectTransform.position;

        // Mettre à jour l'image de la carte sélectionnée
        mapImageDisplay.sprite = mapButtons[index].GetComponent<Image>().sprite;
    }

    // Fonction pour sélectionner la carte et charger la scène correspondante
    private void SelectMap()
    {
        // Récupérer les personnages choisis à partir de PlayerPrefs
        string player1Character = PlayerPrefs.GetString("Player1Character", "Default");
        string player2Character = PlayerPrefs.GetString("Player2Character", "Default");

        // Afficher les personnages dans la console pour tester
        Debug.Log("Joueur 1 a choisi : " + player1Character);
        Debug.Log("Joueur 2 a choisi : " + player2Character);

        // Charger la scène correspondante
        string selectedMap = mapButtons[currentIndex].name; // Utiliser le nom du bouton comme le nom de la carte
        SceneManager.LoadScene(selectedMap);
    }
}
