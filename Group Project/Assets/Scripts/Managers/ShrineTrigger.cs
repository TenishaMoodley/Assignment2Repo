using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShrineTrigger : MonoBehaviour
{
	public enum typeOFShrine { cube, sphere, prism }

	[Header("States")]
	[SerializeField] typeOFShrine typeOfShrine;

	[Header("External Scripts")]
    [SerializeField] ChangePlayer changePlayerScript;

	[Header("Unity Handles")]
	[SerializeField] Material defaultSkybox, newSkybox, SkyboxTheSecond, SkyBoxTheFirst;
	[SerializeField] GameObject shrineBeam, shrineText;
	[SerializeField] Text actualtxtColor;
	[SerializeField] Color txtColor;

	[Header("Floats")]
	[SerializeField] float waitTimeTillDisabling;
	[SerializeField] float waitTimeTillEnabling;

	[Header("Integers")]
	public int ActualIndex = -1;
	[SerializeField] int shapeIndex;

	[Header("Generic Elements")]
	[SerializeField] string thisShrine;

	[Header("Booleans")]
	[SerializeField] bool canActivate;
    void Start()
    {
		
        changePlayerScript = FindObjectOfType<ChangePlayer>();
		defaultSkybox = RenderSettings.skybox;
		shrineBeam.SetActive(false);
		shrineText.SetActive(false);

		if (this.typeOfShrine == typeOFShrine.cube)
			ActualIndex = 0;
		if (this.typeOfShrine == typeOFShrine.sphere)
			ActualIndex = 1;
		if (this.typeOfShrine == typeOFShrine.prism)
			ActualIndex = 2;
	}
    private void Update()
    {
        if (GameManager.shrinesTriggered == 1)
        {
            RenderSettings.skybox= SkyBoxTheFirst;
        }
        if (GameManager.shrinesTriggered == 2)
        {
            RenderSettings.skybox = SkyboxTheSecond;
        }

		if (canActivate && Input.GetKeyDown(KeyCode.LeftControl) && changePlayerScript.checker / shapeIndex == 1)
		{
			GameManager.instance.colourCollected[ActualIndex] = true;
			GameManager.instance.UpdateBar();

			//Play Particle/Animation/Camera Pan

			//Store Colour Permanently
			PlayerPrefs.SetInt(thisShrine, 1); //Returns True, we will cal this in the hunter to make sure they cannot take this colour

			//Confirm Playerprefs
			Debug.Log("Player has " + thisShrine + PlayerPrefs.HasKey(thisShrine));
			GameManager.shrinesTriggered++;
			Debug.Log("Shrines Triggered: " + GameManager.shrinesTriggered);

			StartCoroutine(DisableObject());

			//Disable Text
			shrineText.SetActive(false);

			//Enable Beam
			shrineBeam.SetActive(true);
			//Here we need to sort through the skyboxes
			if (GameManager.shrinesTriggered == 3)
				RenderSettings.skybox = newSkybox;

			this.GetComponent<Collider>().enabled = false;

		}
	}

    private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && changePlayerScript.hasColour[ActualIndex + 1])
		{
			canActivate = true;
			shrineText.SetActive(true);
			actualtxtColor.color = txtColor;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		canActivate = false;
		shrineText.SetActive(false);
	}
	IEnumerator DisableObject()
	{
		yield return new WaitForSeconds(waitTimeTillEnabling);
		Debug.Log("Update UI Element");

		yield return new WaitForSeconds(waitTimeTillDisabling);
		
	}
}
