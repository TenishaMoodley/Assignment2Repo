using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColour : MonoBehaviour
{
    [Header("GameObjects")]
    public ParticleSystem Particles;
    public Light light;

    [Header("Colours")]
    public Color DefaultLight, NewLight;
    public Gradient Default, New;

    [Header("Integers")]
    [SerializeField] [Range(0, 2)] int triggerIndex;

    [Header("External Scripts")]
    [SerializeField] ShrineTrigger[] shrineTrigger = new ShrineTrigger[3];

    private void Awake()
    {
        shrineTrigger = FindObjectsOfType<ShrineTrigger>();
    }
   
    void Start()
    {
        light.color = NewLight;
        var m = Particles.colorOverLifetime;
        m.color = New;
    }

    void Update()
    {
        if (GameManager.instance.colourCollected[shrineTrigger[triggerIndex].ActualIndex])
        {
            var m = Particles.colorOverLifetime;
            m.color = Default;
            light.color = DefaultLight;
        }
    }
}
