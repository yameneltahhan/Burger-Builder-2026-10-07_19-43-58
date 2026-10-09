
using UnityEngine;
using TMPro;

public class Chronometre : MonoBehaviour
{
    [Header("Chronomètre")]
    public float tempsRestant = 120f;
    public TMP_Text texteChronometre;

    [Header("Game Over")]
    public GameOverManager gameOverManager;

    [Header("Score")]
    public BurgerManager burgerManager;

    [Header("Gestion des sons")]
    public AudioManager audioManager;

    private bool jeuTermine = false;

    void Start()
    {
        AfficherTemps();
    }

    void Update()
    {
        // Si la partie est terminée, ne rien faire
        if (jeuTermine)
        {
            return;
        }

        // Faire diminuer le temps
        tempsRestant -= Time.deltaTime;

        // Vérifier si le temps est écoulé
        if (tempsRestant <= 0f)
        {
            tempsRestant = 0f;
            jeuTermine = true;

            AfficherTemps();

            // Jouer le réveil une seule fois
            if (audioManager != null)
            {
                audioManager.JouerSonGameOver();
            }

            // Empêcher le joueur de continuer
            burgerManager.partieTerminee = true;

            // Récupérer le score final
            int scoreFinal = burgerManager.totalEtoiles;

            // Afficher le panneau Game Over
            gameOverManager.AfficherGameOver(scoreFinal);

            Debug.Log("Game Over déclenché !");
            return;
        }

        AfficherTemps();
    }

    void AfficherTemps()
    {
        int secondesTotales = Mathf.CeilToInt(tempsRestant);

        int minutes = secondesTotales / 60;
        int secondes = secondesTotales % 60;

        texteChronometre.text =
            minutes.ToString("00") + ":" +
            secondes.ToString("00");
    }
}
