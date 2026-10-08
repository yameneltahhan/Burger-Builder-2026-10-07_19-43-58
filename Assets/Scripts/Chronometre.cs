
using UnityEngine;
using TMPro;

public class Chronometre : MonoBehaviour
{
    public float tempsRestant = 120f;
    public TMP_Text texteChronometre;

    private bool jeuTermine = false;

    void Start()
    {
        AfficherTemps();
    }

    void Update()
    {
        if (jeuTermine)
        {
            return;
        }

        tempsRestant -= Time.deltaTime;

        if (tempsRestant <= 0f)
        {
            tempsRestant = 0f;
            jeuTermine = true;

            Debug.Log("Temps écoulé ! Game Over !");
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
