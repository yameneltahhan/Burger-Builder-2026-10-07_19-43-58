
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    [Header("Panneau Pause")]
    public GameObject panneauPause;

    [Header("Animation")]
    public float dureeDescente = 0.7f;
    public float dureeMontee = 0.5f;
    public float distance = 1200f;

    private RectTransform rectPanneau;
    private Vector2 positionFinale;
    private bool enPause = false;
    private Coroutine animation;

    void Start()
    {
        Time.timeScale = 1f;

        rectPanneau = panneauPause.GetComponent<RectTransform>();
        positionFinale = rectPanneau.anchoredPosition;

        panneauPause.SetActive(false);
    }

    public void MettreEnPause()
    {
        if (enPause) return;

        enPause = true;
        Time.timeScale = 0f;

        panneauPause.SetActive(true);

        animation = StartCoroutine(Descendre());
    }

    IEnumerator Descendre()
    {
        Vector2 depart = positionFinale + Vector2.up * distance;

        rectPanneau.anchoredPosition = depart;

        float temps = 0f;

        while (temps < dureeDescente)
        {
            temps += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(temps / dureeDescente);
            t = 1f - Mathf.Pow(1f - t, 3f);

            rectPanneau.anchoredPosition =
                Vector2.Lerp(depart, positionFinale, t);

            yield return null;
        }

        rectPanneau.anchoredPosition = positionFinale;
        animation = null;
    }

    public void Reprendre()
    {
        if (!enPause) return;

        if (animation != null)
            StopCoroutine(animation);

        animation = StartCoroutine(Monter());
    }

    IEnumerator Monter()
    {
        Vector2 depart = rectPanneau.anchoredPosition;
        Vector2 arrivee = positionFinale + Vector2.up * distance;

        float temps = 0f;

        while (temps < dureeMontee)
        {
            temps += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(temps / dureeMontee);
            t = t * t * (3f - 2f * t);

            rectPanneau.anchoredPosition =
                Vector2.Lerp(depart, arrivee, t);

            yield return null;
        }

        rectPanneau.anchoredPosition = arrivee;
        panneauPause.SetActive(false);

        enPause = false;
        animation = null;
        Time.timeScale = 1f;
    }

    public void Recommencer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BurgerGame");
    }

    public void RetourMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
