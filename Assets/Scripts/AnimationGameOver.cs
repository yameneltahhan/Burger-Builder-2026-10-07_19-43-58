
using UnityEngine;
using System.Collections;

public class AnimationGameOver : MonoBehaviour
{
    public float duree = 0.6f;
    public float distance = 800f;

    private RectTransform panneau;
    private Vector2 positionFinale;

    void Awake()
    {
        panneau = GetComponent<RectTransform>();
        positionFinale = panneau.anchoredPosition;
    }

    void OnEnable()
    {
        if (panneau == null)
            return;

        StartCoroutine(Animer());
    }

    IEnumerator Animer()
    {
        Vector2 depart = positionFinale + Vector2.up * distance;
        panneau.anchoredPosition = depart;

        float temps = 0f;

        while (temps < duree)
        {
            temps += Time.unscaledDeltaTime;

            float progression = Mathf.Clamp01(temps / duree);

            // Petit effet de rebond
            float mouvement = 1f -
                Mathf.Pow(1f - progression, 3f);

            panneau.anchoredPosition =
                Vector2.LerpUnclamped(depart, positionFinale, mouvement);

            yield return null;
        }

        panneau.anchoredPosition = positionFinale;
    }
}
