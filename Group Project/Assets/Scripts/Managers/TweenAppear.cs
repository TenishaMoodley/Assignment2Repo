using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAppear : MonoBehaviour
{
    public float TweenTime;

    public void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), TweenTime).setEaseInSine();
    }
}
