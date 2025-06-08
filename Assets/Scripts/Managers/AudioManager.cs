using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    public AudioClip OnDeathSound;
    public AudioSource ice;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
}
