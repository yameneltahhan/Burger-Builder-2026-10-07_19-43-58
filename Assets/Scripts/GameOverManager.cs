
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("Panneaux Game Over")]
    public GameObject panneauTryAgain;
    public GameObject panneauNotBad;
    public GameObject panneauGoodJob;
    public GameObject panneauAmazing;

    private bool partieTerminee = false;

    void Start()
    {
        CacherPanneaux();
    }

    public void CacherPanneaux()
    {
        panneauTryAgain.SetActive(false);
        panneauNotBad.SetActive(false);
        panneauGoodJob.SetActive(false);
        panneauAmazing.SetActive(false);
    }

    public void AfficherGameOver(int totalEtoiles)
    {
        if (partieTerminee)
            return;

        partieTerminee = true;

        CacherPanneaux();

        if (totalEtoiles >= 45)
        {
            panneauAmazing.SetActive(true);
        }
        else if (totalEtoiles >= 40)
        {
            panneauGoodJob.SetActive(true);
        }
        else if (totalEtoiles >= 35)
        {
            panneauNotBad.SetActive(true);
        }
        else
        {
            panneauTryAgain.SetActive(true);
        }

        Debug.Log("Game Over ! Étoiles : " + totalEtoiles);
    }
}
