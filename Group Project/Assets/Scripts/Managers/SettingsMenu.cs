using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Settings page where player can change the volume settings,
/// Game quality and,
/// Screen size
/// </summary>
public class SettingsMenu : MonoBehaviour
{

    public Slider currVolume;
    public AudioSource ThemeMusic;

    /*public Sound [] sounds;
    public AudioSource ESC;
    public AudioSource Morphing;
    public AudioSource Collecting;
    public AudioSource Hiding;
    public AudioSource LostColour;
    public AudioSource Activate;*/


    /// <summary>
    /// allows player to change volume of music in settings menu through slider
    /// </summary>
    public void Update()
    {
        SettingsVolume();
    }

    public void SettingsVolume()
    {

       /* foreach (Sound soundCurrentlyLookingAt in sounds)
        {
            soundCurrentlyLookingAt.AudioSource = gameObject.AddComponent<AudioSource>();

            soundCurrentlyLookingAt.volume = currVolume.value;

        }*/

        ThemeMusic.volume = currVolume.value;


        /*ESC.volume = currVolume.value;
        Morphing.volume = currVolume.value;
        Collecting.volume = currVolume.value;
        Hiding.volume = currVolume.value;
        LostColour.volume = currVolume.value;
        Activate.volume = currVolume.value;*/

        AudioListener.pause = false;
    }

    public void Back()
    {
        FindObjectOfType<MusicManager>().Play("ESC Button");
    }

}
