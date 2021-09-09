using System.Collections;
using System.Collections.Generic;
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
	

	[Header("Floats")]
	[SerializeField] float waitTimeTillDisabling;
	[SerializeField] float waitTimeTillEnabling;

	[Header("Integers")]
	public int ActualIndex = -1;

	[Header("Generic Elements")]
	[SerializeField] string thisShrine;
    void Start()
    {
		
        changePlayerScript = FindObjectOfType<ChangePlayer>();
		defaultSkybox = RenderSettings.skybox;


		if (typeOfShrine == typeOFShrine.cube)
			ActualIndex = 0;
		else if (typeOfShrine == typeOFShrine.sphere)
			ActualIndex = 1;
		else if (typeOfShrine == typeOFShrine.prism)
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
    }

    private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && changePlayerScript.hasColour[ActualIndex + 1])
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

			//Here we need to sort through the skyboxes
			if (GameManager.shrinesTriggered == 3)
				RenderSettings.skybox = newSkybox;

			this.GetComponent<Collider>().enabled = false;

			
		}
	}

	IEnumerator DisableObject()
	{
		yield return new WaitForSeconds(waitTimeTillEnabling);
		Debug.Log("Update UI Element");

		yield return new WaitForSeconds(waitTimeTillDisabling);
		
	}
}
