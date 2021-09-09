using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip clip;

    //sliders added with the Range attribute
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool Loop;

    public bool PlayOnAwake;

    [HideInInspector]
    public AudioSource AudioSource;
}
