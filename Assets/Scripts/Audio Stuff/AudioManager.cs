using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource music, sfx;

    public AudioClip background;
    public AudioClip knife;

    public AudioClip garlic;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public AudioClip death;

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void Start()
    {
        /* Background music*/
        // Check if 'music' is not assigned and attempt to find it
        if (music == null)
        {
            music = GetComponent<AudioSource>();
            if (music == null)
            {
                Debug.LogError("The 'music' AudioSource is not assigned and could not be found on the AudioManager GameObject.");
            }
        }

        if (music != null)
        {
            music.clip = background;
            music.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    /* Button SFX */
    public void HoverSound()
    {
        sfx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        sfx.PlayOneShot(clickFx);
    }
}
