using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    public AudioSource musicSource;
    public AudioSource soundFXsource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        musicSource.pitch = Random.Range(.95f, 1.05f);
    }

    public void PlayVariation(AudioClip clip)
    {
        soundFXsource.pitch = Random.Range(.95f, 1.05f);
        soundFXsource.PlayOneShot(clip, 1f);
    }
}
