using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonsGameOver : MonoBehaviour
{
    public void RetourMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Rejouer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BurgerGame");
    }
}