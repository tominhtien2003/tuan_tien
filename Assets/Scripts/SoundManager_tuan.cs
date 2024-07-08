using System.Collections.Generic;
using UnityEngine;

public class SoundManager_tuan : MonoBehaviour
{
    public static SoundManager_tuan instance;
    [SerializeField] private List<Sound_tuan> sounds;
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        foreach (var sound in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound.audioClip;
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.playOnAwake = false;
            audioSources[sound.name] = audioSource;
        }

        PlayAudio("Film");
    }

    public void PlayAudio(string name)
    {
        if (audioSources.ContainsKey(name))
        {
            audioSources[name].Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void StopAudio(string name)
    {
        if (audioSources.ContainsKey(name))
        {
            audioSources[name].Stop();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public bool IsPlaying(string name)
    {
        if (audioSources.ContainsKey(name))
        {
            return audioSources[name].isPlaying;
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
            return false;
        }
    }
}
