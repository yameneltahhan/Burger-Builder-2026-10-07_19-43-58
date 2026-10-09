
using UnityEngine;
using System.Collections;

public class IntroductionJeu : MonoBehaviour
{
    [Header("Panneau")]
    public RectTransform panneauIntroduction;

    [Header("Animation")]
    public float dureeDescente = 1f;
    public float tempsAffichage = 4f;
    public float dureeMontee = 0.6f;
    public float distance = 1200f;

    [Header("Jeu")]
    public Chronometre chronometre;
    public BurgerManager burgerManager;

    private Vector2 positionCentre;

    IEnumerator Start()
    {
        // Bloquer le jeu pendant l'introduction
        chronometre.enabled = false;
        burgerManager.enabled = false;

        positionCentre = panneauIntroduction.anchoredPosition;
        Vector2 positionHaut =
            positionCentre + Vector2.up * distance;

        panneauIntroduction.anchoredPosition = positionHaut;

        // Faire descendre le panneau
        yield return Deplacer(positionHaut, positionCentre, dureeDescente);

        // Laisser le joueur lire pendant 4 secondes
        yield return new WaitForSecondsRealtime(tempsAffichage);

        // Faire remonter le panneau
        yield return Deplacer(positionCentre, positionHaut, dureeMontee);

        panneauIntroduction.gameObject.SetActive(false);

        // Démarrer la partie
        burgerManager.enabled = true;
        chronometre.enabled = true;
    }

    IEnumerator Deplacer(Vector2 depart, Vector2 arrivee, float duree)
    {
        float temps = 0f;

        while (temps < duree)
        {
            temps += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(temps / duree);
            t = t * t * (3f - 2f * t);

            panneauIntroduction.anchoredPosition =
                Vector2.Lerp(depart, arrivee, t);

            yield return null;
        }

        panneauIntroduction.anchoredPosition = arrivee;
    }
}
