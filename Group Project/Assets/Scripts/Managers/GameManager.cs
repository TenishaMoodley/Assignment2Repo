using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("Singleton && Static Values")]
	public static GameManager instance;
	public static int shrinesTriggered;

	[Header("Unity Handles")]
	[SerializeField] GameObject pausePanel;
	[SerializeField] GameObject ggPanel;
	[SerializeField] Image currentImage, ProgressBar;
	[SerializeField] Sprite[] images;
	public AudioSource FinalTheme;
	public AudioSource theme;

	[Header("Booleans")]
	[SerializeField] bool paused;
	[SerializeField] bool isGameOver;

	[Header("Shrine Booleans")]
	public bool[] colourCollected = new bool[3] { false, false, false };

	[Header("Floats")]
	[SerializeField] float currentValue;
	[SerializeField] float valueToAdd = 0.333f, fillTIme, totalValue, lerpProgressColorTime, waitingSeconds, progressValue = 0.90f;
	[Range(0f, 1f)] public float lerpColour = 0.35f;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(this);
			instance = this;
		}

		PlayerPrefs.DeleteAll();
	}
	private void Start()
	{
		theme = GameObject.Find("Theme Song").GetComponent<AudioSource>();
		FinalTheme = GameObject.Find("Final Theme Song").GetComponent<AudioSource>();
		FinalTheme.enabled = false;

		ggPanel.SetActive(false);
		pausePanel.SetActive(false);
		SwitchImages(0, new Color(255, 255, 255, 0.29f));
		ProgressBar.fillAmount = 0;
		currentValue = ProgressBar.fillAmount;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !paused)
		{
			PauseGame();

			//Sound for ESC button
			FindObjectOfType<MusicManager>().Play("ESC Button");
		}

		if (currentValue != ProgressBar.fillAmount)
			ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, totalValue, Time.deltaTime * fillTIme);
		
		if (ProgressBar.fillAmount > progressValue)
		{
			ProgressBar.fillAmount = 1f;

			//Tween Animation! && aybe Play Particles Here
			ProgressBar.GetComponentInParent<TweenProgressBar>().TweenPunch();

			//Change Colour
			ProgressBar.color = Color.Lerp(ProgressBar.color, Color.green, Time.deltaTime * lerpProgressColorTime);

			//Play/call GGS or Tweenin! and this makes ssure it only plays once
			if (waitingSeconds <= 0 && !isGameOver)
				GGs();
			else if(!isGameOver)
				waitingSeconds -= Time.deltaTime;

		}

		
	}

	#region Pause Logic
	public void Resume()
	{
		FindObjectOfType<MusicManager>().Play("ESC Button");
		pausePanel.SetActive(false);
		paused = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void PauseGame()
	{
		Debug.Log("Paused");
		pausePanel.SetActive(true);
		paused = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void ExitGame()
	{
		Debug.Log("GGs");
		FindObjectOfType<MusicManager>().Play("ESC Button");
		Application.Quit();
	}
	#endregion

	#region UI_Switching && Progress Bar
	public void SwitchImages(int index, Color myColour)
	{
		currentImage.sprite = images[index];
		currentImage.color = myColour;
	}

	public void UpdateBar()
	{
		//Tweening can go here

		currentValue = ProgressBar.fillAmount;
		totalValue = currentValue + valueToAdd;

		//Tween Animation! && aybe Play Particles Here
		ProgressBar.GetComponentInParent<TweenProgressBar>().TweenPunch();

		ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, totalValue, Time.deltaTime * fillTIme);
	}

	void GGs()
	{
		ggPanel.SetActive(true);
		waitingSeconds = 2.5f;

		isGameOver = true;

		StartCoroutine(DisableGGs());
	}

	IEnumerator DisableGGs()
	{
		yield return new WaitForSeconds(waitingSeconds);
			ggPanel.SetActive(false);

		if (shrinesTriggered == 3)
		{
			theme.enabled = false;
			FinalTheme.enabled = true;

		}
		else
		{
			theme.enabled = true;
			FinalTheme.enabled = false;
		}

	}
	#endregion
}
