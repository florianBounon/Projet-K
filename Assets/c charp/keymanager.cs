using UnityEngine;
using UnityEngine.InputSystem;  

public class keymanager : MonoBehaviour
{
    public static keymanager instance;  // Instance Singleton pour l'accès global

    public PlayerControls player1Controls;
    public PlayerControls player2Controls;

    private bool isPlayer1UsingGamepad;
    private bool isPlayer2UsingGamepad;

    void Awake()
    {
        // Assurez-vous qu'il n'y ait qu'une seule instance de keymanager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Empêcher la destruction de l'objet lors du changement de scène
        }
        else
        {
            Destroy(gameObject);  // Supprimer les doublons de keymanager
        }
    }

    void Start()
    {
        // Détection si un gamepad est connecté
        isPlayer1UsingGamepad = Gamepad.all.Count > 0 && Gamepad.all[0].enabled;
        isPlayer2UsingGamepad = Gamepad.all.Count > 1 && Gamepad.all[1].enabled;

        // Charger les préférences de contrôles depuis PlayerPrefs
        LoadPlayerControls();
    }

    void LoadPlayerControls()
    {
        // Si une manette est utilisée, ajuster les contrôles
        if (isPlayer1UsingGamepad)
        {
            player1Controls.droite = "Gamepad_Right";  // Correspond au bouton de la manette
            player1Controls.gauche = "Gamepad_Left";  // Correspond au bouton de la manette
            player1Controls.jump = "Gamepad_A";      // A, B, X, Y sont des boutons courants
            player1Controls.crouch = "Gamepad_Down";
            player1Controls.attackkey = "Gamepad_B"; 
            player1Controls.kickkey = "Gamepad_X";
        }
        else
        {
            player1Controls.droite = PlayerPrefs.GetString("Player1RightKey", "d");
            player1Controls.gauche = PlayerPrefs.GetString("Player1LeftKey", "a");
            player1Controls.jump = PlayerPrefs.GetString("Player1JumpKey", "w");
            player1Controls.crouch = PlayerPrefs.GetString("Player1CrouchKey", "s");
            player1Controls.attackkey = PlayerPrefs.GetString("Player1AttackKey", "e");
            player1Controls.kickkey = PlayerPrefs.GetString("Player1KickKey", "f");
        }

        // Répéter pour le joueur 2
        if (isPlayer2UsingGamepad)
        {
            player2Controls.droite = "Gamepad_Right";
            player2Controls.gauche = "Gamepad_Left";
            player2Controls.jump = "Gamepad_A";
            player2Controls.crouch = "Gamepad_Down";
            player2Controls.attackkey = "Gamepad_B";
            player2Controls.kickkey = "Gamepad_X";
        }
        else
        {
            player2Controls.droite = PlayerPrefs.GetString("Player2RightKey", ";");
            player2Controls.gauche = PlayerPrefs.GetString("Player2LeftKey", "k");
            player2Controls.jump = PlayerPrefs.GetString("Player2JumpKey", "o");
            player2Controls.crouch = PlayerPrefs.GetString("Player2CrouchKey", "l");
            player2Controls.attackkey = PlayerPrefs.GetString("Player2AttackKey", "i");
            player2Controls.kickkey = PlayerPrefs.GetString("Player2KickKey", "j");
        }
    }

    public void SavePlayerControls()
    {
        PlayerPrefs.SetString("Player1RightKey", player1Controls.droite);
        PlayerPrefs.SetString("Player1LeftKey", player1Controls.gauche);
        PlayerPrefs.SetString("Player1JumpKey", player1Controls.jump);
        PlayerPrefs.SetString("Player1AttackKey", player1Controls.attackkey);
        PlayerPrefs.SetString("Player1KickKey", player1Controls.kickkey);

        PlayerPrefs.SetString("Player2RightKey", player2Controls.droite);
        PlayerPrefs.SetString("Player2LeftKey", player2Controls.gauche);
        PlayerPrefs.SetString("Player2JumpKey", player2Controls.jump);
        PlayerPrefs.SetString("Player2AttackKey", player2Controls.attackkey);
        PlayerPrefs.SetString("Player2KickKey", player2Controls.kickkey);

        PlayerPrefs.Save();
    }
}
