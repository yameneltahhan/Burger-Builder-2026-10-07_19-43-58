using UnityEngine;
using System.Collections;

public class FlashCommande : MonoBehaviour
{
    public GameObject flashRouge;
    public GameObject flashVert;
    public float dureeFlash = 0.5f;
    private Coroutine flashActuel;

    public void FlashRouge()
    {
        LancerFlash(flashRouge);
    }
    public void FlashVert()
    {
        LancerFlash(flashVert);
    }
    
    void LancerFlash(GameObject flash)
    {
        if (flashActuel != null)
        {
            StopCoroutine(flashActuel);
        }
        flashRouge.SetActive(false);
        flashVert.SetActive(false);

        flashActuel = StartCoroutine(FlashCoroutine(flash));
        flashActuel = StartCoroutine(FlashCoroutine(flash));
    }
    IEnumerator FlashCoroutine(GameObject flash)
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(dureeFlash);
        flash.SetActive(false);
        flashActuel = null;
    }

}
