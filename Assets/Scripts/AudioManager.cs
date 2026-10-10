using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip sonBonneReponse;
    public AudioClip sonMauvaiseReponse;
    public AudioClip sonGameOver;
    public AudioClip Pain;
    public AudioClip Salade;
    public AudioClip Tomate;
    public AudioClip Fromage;
    public AudioClip Viande;
    public AudioClip poisson;
    public AudioClip Onion;
    public AudioClip sauceBurger;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void JouerSonBonneReponse()
    {
        audioSource.PlayOneShot(sonBonneReponse);
    }

    public void JouerSonMauvaiseReponse()
    {
        audioSource.PlayOneShot(sonMauvaiseReponse);
    }

    public void JouerSonGameOver()
    {
        audioSource.PlayOneShot(sonGameOver);
    }

    public void JouerSonIngredient(string nomIngredient)
    {
        AudioClip sonIngredient = null;

        switch (nomIngredient)
        {
            case "Pain":
                sonIngredient = Pain;
                break;
            case "Salade":
                sonIngredient = Salade;
                break;
            case "Tomate":
                sonIngredient = Tomate;
                break;
            case "Fromage":
                sonIngredient = Fromage;
                break;
            case "Viande":
                sonIngredient = Viande;
                break;
            case "Poisson":
                sonIngredient = poisson;
                break;
            case "Oignon":
                sonIngredient = Onion;
                break;
            case "SauceBurger":
                sonIngredient = sauceBurger;
                break;
            default:
                Debug.LogWarning("Aucun son associé à l'ingrédient : " + nomIngredient);
                break;
        }

        if (sonIngredient != null)
        {
            audioSource.PlayOneShot(sonIngredient);
        }
    }

}
