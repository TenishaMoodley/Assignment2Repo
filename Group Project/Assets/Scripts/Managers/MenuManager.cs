using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[Header("Generic Elements")]
	[SerializeField] string sceneName;
	
	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
	}

	public void PlayGame(string scene)
	{
		FindObjectOfType<MusicManager>().Play("ESC Button");
		ClearPlayerPrefs();
		SceneManager.LoadScene(scene);
	}

	public void Tutorial()
	{
		FindObjectOfType<MusicManager>().Play("ESC Button");
		SceneManager.LoadScene(sceneName);
	}

	public void ClearPlayerPrefs()
	{
		FindObjectOfType<MusicManager>().Play("ESC Button");
		PlayerPrefs.DeleteAll();
	}
	public void ExitGame()
	{
		FindObjectOfType<MusicManager>().Play("ESC Button");
		Debug.Log("GGs man!");
		Application.Quit();
	}
}
