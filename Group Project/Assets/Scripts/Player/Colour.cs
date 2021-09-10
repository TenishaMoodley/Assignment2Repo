using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{
    [Header("External Scripts")]
    [SerializeField]ShrineTrigger[] shrineTrigger = new ShrineTrigger[3];

    [Header("Generic Elements")]
    public List<Color> defaultColors = new List<Color>();
    public List<Color> newColours = new List<Color>();

    [Header("Unity Handles")]
    public Material[] colours, defaultColor;
    public MeshRenderer rend;

    [Header("Integers")]
    [SerializeField] [Range(0, 2)] int triggerIndex;
	private void Awake()
	{
            shrineTrigger = FindObjectsOfType<ShrineTrigger>();
	}

	private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        for (int i = 0; i < colours.Length; i++)
        {

            rend.sharedMaterials[i].color = newColours[i];
        }
    }

	private void Update()
	{
        
        for (int i = 0; i < defaultColor.Length; i++)
        {
            if (GameManager.instance.colourCollected[shrineTrigger[triggerIndex].ActualIndex])
            {

               rend.sharedMaterials[i].color = Color.Lerp(rend.sharedMaterials[i].color, defaultColors[i], GameManager.instance.lerpColour * Time.deltaTime);

            }

        }
    }
}
