
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

    private bool jeuTermine = false;

    void Start()
    {
        AfficherTemps();
    }

    void Update()
    {
        if (jeuTermine)
            return;

        tempsRestant -= Time.deltaTime;

        if (tempsRestant <= 0f)
        {
            tempsRestant = 0f;
            jeuTermine = true;
            burgerManager.partieTerminee = true;


            AfficherTemps();

            int scoreFinal = burgerManager.totalEtoiles;
            gameOverManager.AfficherGameOver(scoreFinal);

            Debug.Log("Temps terminé !");
            return;
        }

        AfficherTemps();
    }

    void AfficherTemps()
    {
        int secondesTotales = Mathf.CeilToInt(tempsRestant);

        int minutes = secondesTotales / 60;
        int secondes = secondesTotales % 60;

        if (texteChronometre != null)
        {
            texteChronometre.text =
                minutes.ToString("00") + ":" +
                secondes.ToString("00");
        }
    }
}
