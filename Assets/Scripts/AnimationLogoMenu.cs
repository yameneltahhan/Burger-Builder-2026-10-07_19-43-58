
using UnityEngine;
using System.Collections;

public class AnimationLogoMenu : MonoBehaviour
{
    public float duree = 1f;
    public float distance = 1000f;

    private RectTransform rect;
    private Vector2 positionFinale;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        positionFinale = rect.anchoredPosition;
    }

    IEnumerator Start()
    {
        Vector2 depart = positionFinale + Vector2.up * distance;
        rect.anchoredPosition = depart;

        float temps = 0f;

        while (temps < duree)
        {
            temps += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(temps / duree);
            t = t * t * (3f - 2f * t);

            rect.anchoredPosition =
                Vector2.Lerp(depart, positionFinale, t);

            yield return null;
        }

        rect.anchoredPosition = positionFinale;
    }
}
