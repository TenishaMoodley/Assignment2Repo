using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Playables;
using UnityEngine;

public class StoryContextManager : MonoBehaviour
{
    [Header("Generic Elements")]
    [SerializeField] string sceneName;
    [SerializeField] string firstText, lastText;

    [Header("Floats")]
    [SerializeField] float countdownTime = 5f;
    [SerializeField] float storyTime, storyDelay = 2.2f;
    [SerializeField] float timeToSkip = 1.2f;
    [SerializeField] float currentTimeToSkip;

    [Header("Booleans")]
    [SerializeField] bool canMoveOn;

    [Header("Unity Handles")]
    [SerializeField] TextMeshProUGUI skipTxt;
    [SerializeField] TextMeshProUGUI RestartTxt;
    [SerializeField] PlayableDirector dir;

    private void Start()
	{
        RestartTxt.text = "";

        currentTimeToSkip = timeToSkip;
        canMoveOn = false;

        storyTime = (float)dir.duration;
        StartCoroutine(PlayStory());
    }
	// Update is called once per frame
	void Update()
    {
        InputCheck();

        /*
         * We might delete this
        if(canMoveOn)
		{
            StartCoroutine(StartCountdown());
		}*/
    }

    void CallNextScene()
	{
        SceneManager.LoadScene(sceneName);
	}

    void InputCheck()
    {
        //Button To Press to Skip
        if (Input.GetKey(KeyCode.Space))
        {
            timeToSkip -= Time.deltaTime;
            if (timeToSkip <= 0)
            {
                Debug.Log("Loaded");
                CallNextScene();
            }
        }
        else
        {
            timeToSkip = currentTimeToSkip;
        }

        //Skip
        if(Input.GetKeyDown(KeyCode.R))
		{
            dir.Stop();
            dir.time = 0;
            dir.Play();
            dir.Evaluate();
		}
    }

    IEnumerator StartCountdown()
	{
        yield return new WaitForSeconds(countdownTime);
        CallNextScene();
	}

    IEnumerator PlayStory()
	{
        //Delay before story begins/ we can have the player press a button to skip this part
        yield return new WaitForSeconds(storyDelay);

        //Play story
        dir.Play();

        yield return new WaitForSeconds(storyTime);
        skipTxt.text = firstText;
        RestartTxt.text = lastText;

        if (Input.GetKeyDown(KeyCode.Space))
            CallNextScene();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restart");
        }

        //can change
        canMoveOn = true;
	}
}
