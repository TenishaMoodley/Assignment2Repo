using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TweenProgressBar : MonoBehaviour
{
    public float TweenTime;
    public float Strength;

    public void TweenPunch()
    {
        LeanTween.cancel(gameObject);

        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * Strength, TweenTime).setEasePunch();
    }

}

