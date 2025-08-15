using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;// Reference to the AudioSource component

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();// Reference to the AudioSource component

        // Making sure it doesn't get destroyed on scene load
        DontDestroyOnLoad(gameObject);
    }

    public void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Pause();
    }

    public void ResumeMusic()
    {
        if (audioSource != null)
            audioSource.UnPause();
    }
}
