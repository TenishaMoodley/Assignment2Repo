using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTextManager : MonoBehaviour
{
    public float TweenTime;
    public float Strength;

    private void Start()
    {
        TweenPunch();
    }

    public void OnEnable()
    {
        TweenPunch();
    }

    public void TweenPunch()
    {
        LeanTween.cancel(gameObject);

        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * Strength, TweenTime).setEasePunch();
    }

}

