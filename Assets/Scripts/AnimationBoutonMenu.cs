
using UnityEngine;
using System.Collections;

public class AnimationBoutonMenu : MonoBehaviour
{
    public float duree = 0.5f;
    public float delai = 0f;

    private RectTransform rect;
    private Vector3 tailleFinale;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        tailleFinale = rect.localScale;
    }

    IEnumerator Start()
    {
        rect.localScale = Vector3.zero;

        yield return new WaitForSecondsRealtime(delai);

        float temps = 0f;

        while (temps < duree)
        {
            temps += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(temps / duree);
            float zoom = 1f - Mathf.Pow(1f - t, 3f);

            rect.localScale =
                Vector3.LerpUnclamped(Vector3.zero, tailleFinale, zoom);

            yield return null;
        }

        rect.localScale = tailleFinale;
    }
}
