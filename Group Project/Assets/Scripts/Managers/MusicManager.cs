using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// All sounds and music in this game come from ZapSplat.com
/// </summary>
public class MusicManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {

        foreach (Sound soundCurrentlyLookingAt in sounds)
        {
            soundCurrentlyLookingAt.AudioSource = gameObject.AddComponent<AudioSource>();

            //clip
            soundCurrentlyLookingAt.AudioSource.clip = soundCurrentlyLookingAt.clip;

            //volume
            soundCurrentlyLookingAt.AudioSource.volume = soundCurrentlyLookingAt.volume;

            //pitch
            soundCurrentlyLookingAt.AudioSource.pitch = soundCurrentlyLookingAt.pitch;

            //loop
            soundCurrentlyLookingAt.AudioSource.loop = soundCurrentlyLookingAt.Loop;

            soundCurrentlyLookingAt.AudioSource.playOnAwake = soundCurrentlyLookingAt.PlayOnAwake;

        }
    }

    /// <summary>
    /// looks through the array sounds to find the name of the called sound
    /// </summary>
    public void Play(string name)
    {
        Sound SoundThatWeFind = Array.Find(sounds, sound => sound.Name == name);
        SoundThatWeFind.AudioSource.Play();

        if (SoundThatWeFind == null)
        {
            Debug.LogWarning("Music reference " + name + " is not found");
            return;
        }
    }
}