using UnityEngine;

[System.Serializable]
public class Sound_tuan
{
    public string name;
    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.0f, 1.0f)]
    public float pitch;
    [Range(0.0f, 1.0f)]
    public float yaw;
    [Range(0.0f, 1.0f)]
    public float roll;
    public AudioClip audioClip;
    [HideInInspector]
    public AudioSource audioSource;
}
