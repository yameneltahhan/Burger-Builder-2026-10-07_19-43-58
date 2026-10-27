
using UnityEngine;
using System.Collections;

public class AnimationCredits : MonoBehaviour
{
    [Header("Panneau")]
    public RectTransform panneau;

    [Header("Animation")]
    public float dureeDescente = 0.7f;
    public float dureeMontee = 0.5f;
    public float distance = 1200f;

    private Vector2 positionFinale;
    private Coroutine animation;

    void Awake()
    {
        if (panneau == null)
            panneau = GetComponent<RectTransform>();

        positionFinale = panneau.anchoredPosition;
    }

    public void Ouvrir()
    {
        if (animation != null)
            StopCoroutine(animation);

        gameObject.SetActive(true);
        animation = StartCoroutine(Descendre());
    }

    IEnumerator Descendre()
    {
        Vector2 depart = positionFinale + Vector2.up * distance;
        panneau.anchoredPosition = depart;

        float temps = 0f;

        while (temps < dureeDescente)
        {
            temps += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(temps / dureeDescente);
            t = 1f - Mathf.Pow(1f - t, 3f);

            panneau.anchoredPosition =
                Vector2.Lerp(depart, positionFinale, t);

            yield return null;
        }

        panneau.anchoredPosition = positionFinale;
        animation = null;
    }

    public void Fermer()
    {
        if (animation != null)
            StopCoroutine(animation);

        animation = StartCoroutine(Monter());
    }

    IEnumerator Monter()
    {
        Vector2 depart = panneau.anchoredPosition;
        Vector2 arrivee = positionFinale + Vector2.up * distance;

        float temps = 0f;

        while (temps < dureeMontee)
        {
            temps += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(temps / dureeMontee);
            t = t * t * (3f - 2f * t);

            panneau.anchoredPosition =
                Vector2.Lerp(depart, arrivee, t);

            yield return null;
        }

        panneau.anchoredPosition = arrivee;
        animation = null;
        gameObject.SetActive(false);
    }
}
