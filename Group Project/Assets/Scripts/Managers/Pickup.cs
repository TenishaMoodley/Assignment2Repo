using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum typeOFPickUp { red, yellow, blue }

    [Header("States")]
    [SerializeField] typeOFPickUp pickUp;

    [Header("External Scripts")]
    public ChangePlayer changePlayerScript;

    [Header("Booleans")]
    public bool isColour;
    public bool shapeColour;

    [Header("Integers")]
    public int ActualIndex = -1;

    [Header("Unity Handles")]
    public GameObject Beam;
    public AudioSource theme;
    

    [Header("Floats")]
    public float currPitch;
    public float lerpTime = 0.5f, finalPitch;

    [Header("Texts game Objects")]
    public GameObject ShapeChangeText;
    public GameObject ScrollWheelText;

    // Start is called before the first frame update
    void Start()
    {
        theme = GameObject.Find("Theme Song").GetComponent<AudioSource>();
        
        ShapeChangeText.SetActive(false);
        ScrollWheelText.SetActive(false);

        isColour = false;

        if (pickUp == typeOFPickUp.red)
            ActualIndex = 0;
        else if (pickUp == typeOFPickUp.yellow)
            ActualIndex = 1;
        else if (pickUp == typeOFPickUp.blue)
            ActualIndex = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColour)
        {
            Beam.SetActive(true);
            gameObject.SetActive(true);
        }

     // changePlayerScript.hasColour[ActualIndex + 1] = isColour;
     // changePlayerScript.hasShapeColour[ActualIndex] = isColour;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player" && !isColour)
        {
            //Play Sound
            FindObjectOfType<MusicManager>().Play("Notification");

            ShapeChangeText.SetActive(true);
            ScrollWheelText.SetActive(true);
            Debug.Log("Colour Obtain");
            isColour = true;
            shapeColour = true;
            changePlayerScript.hasColour[ActualIndex + 1] = isColour;
            changePlayerScript.hasShapeColour[ActualIndex] = shapeColour;

            if (pickUp == typeOFPickUp.red)
                ChangePlayer.ColourIndex = 1;
            else if (pickUp == typeOFPickUp.yellow)
                ChangePlayer.ColourIndex = 2;
            else if (pickUp == typeOFPickUp.blue)
                ChangePlayer.ColourIndex = 3;

            changePlayerScript.CheckColour();

            //Sound for collecting colour/shape
            FindObjectOfType<MusicManager>().Play("Collecting");

            //Pitch Change of Theme
            currPitch = theme.pitch;
            finalPitch = currPitch + 0.25f;

            theme.pitch = finalPitch;

            Beam.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}
