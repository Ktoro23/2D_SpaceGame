using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsFXManager : MonoBehaviour
{
    public static SoundsFXManager Instance { get; private set; }

    public AudioClip OnDeathSound;
    public AudioClip Boosting;
    public AudioClip Pause;
    public AudioClip unPause;
    public AudioClip Hit;
    public AudioClip boom2;
    // public AudioClip hitRock;
    public AudioClip shoot;
    public AudioClip bossHit;
    public AudioClip bossCharge;
    public AudioClip bossSpawn;
    public AudioClip bettleHit;
    public AudioClip beetleDestroy;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [SerializeField] private AudioSource soundFXObject;

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume, float maxDistance = 200)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.maxDistance = maxDistance;

        audioSource.Play();

        float clipLength = audioSource == null ? 0f : audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySoundFXClip(AudioClip audioClip, float volume)
    {
        if (audioClip == null) return;

        AudioSource audioSource = Instantiate(soundFXObject);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.spatialBlend = 0; //set to 2D

        audioSource.Play();

        float clipLength = audioSource == null ? 0f : audioSource.clip.length;


        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume, float maxDistance = 200)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;
        audioSource.maxDistance = maxDistance;

        audioSource.Play();

        float clipLength = audioSource == null ? 0f : audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySoundFXClip(AudioClip audioClip, float volume, bool randomizePitch)
    {
        if (audioClip == null) return;

        AudioSource audioSource = Instantiate(soundFXObject);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        if (randomizePitch)
            audioSource.pitch = Random.Range(0.7f, 1.3f);

        audioSource.spatialBlend = 0; //set to 2D

        audioSource.Play();

        float clipLength = audioSource == null ? 0f : audioSource.clip.length;


        Destroy(audioSource.gameObject, clipLength);
    }
}
