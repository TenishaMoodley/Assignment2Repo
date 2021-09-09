using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChange : MonoBehaviour
{
    [Header("External Scripts")]
    [SerializeField] ShrineTrigger[] shrineTrigger = new ShrineTrigger[3];

    [SerializeField] Texture2D[] TArray;
   [SerializeField] Texture2D[] Default;
   [SerializeField] TerrainLayer[] tlayers;

    [Header("Integers")]
    [SerializeField] [Range(0, 2)] int triggerIndex;

    // Start is called before the first frame update
    void Start()
    {
        shrineTrigger = FindObjectsOfType<ShrineTrigger>();
        tlayers[0].diffuseTexture = Default[0];
        tlayers[1].diffuseTexture = Default[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.colourCollected[shrineTrigger[triggerIndex].ActualIndex])
        {
            tlayers[0].diffuseTexture = TArray[0];
            tlayers[1].diffuseTexture = TArray[1];

        }
    }
}
