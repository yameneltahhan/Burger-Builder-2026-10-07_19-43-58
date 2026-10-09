using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip sonBonneReponse;
    public AudioClip sonMauvaiseReponse;
    public AudioClip sonGameOver;

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

}
