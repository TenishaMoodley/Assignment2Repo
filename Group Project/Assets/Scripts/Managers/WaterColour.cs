using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColour : MonoBehaviour
{
    [Header("Unity Handles")]
    public Material[] waterMaterials = new Material[2];
    public MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material = waterMaterials[0];
       
    }

    private void Update()
    {

            if (GameManager.shrinesTriggered == 3)
                rend.material = waterMaterials[1];      
    }
}
