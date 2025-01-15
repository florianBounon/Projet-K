using UnityEngine;
using TMPro;  // Nécessaire pour utiliser TextMesh Pro
using System.Collections;

public class PunchingBall : MonoBehaviour
{
    public string droite;
    public string gauche;
    public string jump;
    public string attackkey;
    public TextMeshProUGUI degatsTextPrefab;  // Référence au prefab TextMeshProUGUI
    public Transform headPosition; // Position de la tête du personnage
    public Canvas canvas;  // Référence à votre Canvas
    public float heightOffset = 50f;  // Décalage en hauteur (en unités locales du Canvas)

    public void takedmg(int degats)
    {
        // Créer une instance du texte pour afficher les dégâts
        TextMeshProUGUI degatsText = Instantiate(degatsTextPrefab, headPosition.position, Quaternion.identity);

        // Attacher le texte directement au Canvas
        degatsText.transform.SetParent(canvas.transform, false);  // Le "false" garde la position locale intacte

        // Convertir la position du monde (headPosition) en coordonnées locales dans le Canvas
        RectTransform rectTransform = degatsText.GetComponent<RectTransform>();
        rectTransform.position = Camera.main.WorldToScreenPoint(headPosition.position);

        // Ajuster la position Y dans l'espace local du Canvas pour déplacer le texte plus haut ou plus bas
        Vector3 localPosition = rectTransform.localPosition;
        localPosition.y += heightOffset;  // Appliquer le décalage vertical en coordonnées locales

        rectTransform.localPosition = localPosition;  // Appliquer la nouvelle position locale avec le décalage

        // Définir les paramètres du texte pour le rendre visible
        degatsText.text = degats.ToString();
        degatsText.fontSize = 30;  // Taille de police raisonnable
        degatsText.color = Color.white;  // Couleur du texte
        degatsText.alignment = TextAlignmentOptions.Center;  // Centrer le texte

        // Utiliser une coroutine pour détruire le texte après un certain délai
        StartCoroutine(DestroyDamageText(degatsText));
    }

    private IEnumerator DestroyDamageText(TextMeshProUGUI text)
    {
        // Attendre 1 seconde
        yield return new WaitForSeconds(1f);

        // Détruire le texte affiché
        Destroy(text.gameObject);
    }
}
