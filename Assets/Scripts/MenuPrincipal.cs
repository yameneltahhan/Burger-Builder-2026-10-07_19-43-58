using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jouer()
    {
        SceneManager.LoadScene("burgerGame");
    }
}