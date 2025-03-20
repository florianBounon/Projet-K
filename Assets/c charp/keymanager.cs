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
            player1Controls.jump = "Gamepad_Up";      // A, B, X, Y sont des boutons courants
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
            player1Controls.attackkey = PlayerPrefs.GetString("Player1AttackKey", "h");
            player1Controls.kickkey = PlayerPrefs.GetString("Player1KickKey", "j");

            player1Controls.projectile = PlayerPrefs.GetString("Player1ProjectileKey", "k");
            player1Controls.grabkey = PlayerPrefs.GetString("Player1GrabKey", "y");
            player1Controls.parrykey = PlayerPrefs.GetString("Player1ParryKey", "u");
            player1Controls.dashkey = PlayerPrefs.GetString("Player1DashKey", "left shift");
        }

        // Répéter pour le joueur 2
        if (isPlayer2UsingGamepad)
        {
            player2Controls.droite = "Gamepad_Right";
            player2Controls.gauche = "Gamepad_Left";
            player2Controls.jump = "Gamepad_Up";
            player2Controls.crouch = "Gamepad_Down";
            player2Controls.attackkey = "Gamepad_B";
            player2Controls.kickkey = "Gamepad_X";
        }
        else
        {
            player2Controls.droite = PlayerPrefs.GetString("Player2RightKey", "right");
            player2Controls.gauche = PlayerPrefs.GetString("Player2LeftKey", "left");
            player2Controls.jump = PlayerPrefs.GetString("Player2JumpKey", "up");
            player2Controls.crouch = PlayerPrefs.GetString("Player2CrouchKey", "down");
            player2Controls.attackkey = PlayerPrefs.GetString("Player2AttackKey", "[1]");
            player2Controls.kickkey = PlayerPrefs.GetString("Player2KickKey", "[2]");

            player2Controls.projectile = PlayerPrefs.GetString("Player2ProjectileKey", "[3]");
            player2Controls.grabkey = PlayerPrefs.GetString("Player2GrabKey", "[4]");
            player2Controls.parrykey = PlayerPrefs.GetString("Player2ParryKey", "[5]");
            player2Controls.dashkey = PlayerPrefs.GetString("Player2DashKey", "right shift");
        }
    }

    public void SavePlayerControls()
    {
        PlayerPrefs.SetString("Player1RightKey", player1Controls.droite);
        PlayerPrefs.SetString("Player1LeftKey", player1Controls.gauche);
        PlayerPrefs.SetString("Player1JumpKey", player1Controls.jump);
        PlayerPrefs.SetString("Player1AttackKey", player1Controls.attackkey);
        PlayerPrefs.SetString("Player1KickKey", player1Controls.kickkey);

        PlayerPrefs.SetString("Player1ProjectileKey", player1Controls.projectile);
        PlayerPrefs.SetString("Player1GrabKey", player1Controls.grabkey);
        PlayerPrefs.SetString("Player1ParryKey", player1Controls.parrykey);
        PlayerPrefs.SetString("Player1DashKey", player1Controls.dashkey);

        PlayerPrefs.SetString("Player2RightKey", player2Controls.droite);
        PlayerPrefs.SetString("Player2LeftKey", player2Controls.gauche);
        PlayerPrefs.SetString("Player2JumpKey", player2Controls.jump);
        PlayerPrefs.SetString("Player2AttackKey", player2Controls.attackkey);
        PlayerPrefs.SetString("Player2KickKey", player2Controls.kickkey);

        PlayerPrefs.SetString("Player2ProjectileKey", player2Controls.projectile);
        PlayerPrefs.SetString("Player2GrabKey", player2Controls.grabkey);
        PlayerPrefs.SetString("Player2ParryKey", player2Controls.parrykey);
        PlayerPrefs.SetString("Player2DashKey", player2Controls.dashkey);

        PlayerPrefs.Save();
    }
}
