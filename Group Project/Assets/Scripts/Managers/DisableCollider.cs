using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
	[Header("Unity Handles")]
	[SerializeField] Collider[] ColliderObjs;

	[Header("Externa Scripts")]
	HidingMechanic hm;
	ChangePlayer changePlayerScript;

	[Header("Index")]
	[SerializeField] int shapeIndex;
	private void Start()
	{
		hm = FindObjectOfType<HidingMechanic>();
		changePlayerScript = FindObjectOfType<ChangePlayer>();
	}
	private void Update()
	{
		
		if (hm.currentlyHiding)
		{
			if (changePlayerScript.checker / shapeIndex == 1)
			{
				Disbable();
				Debug.Log("Disabled");
			}
		}
		else
		{
			EnableColliders();
		}
	}
	public void Disbable()
	{
		for (int i = 0; i < ColliderObjs.Length; i++)
		{
			ColliderObjs[i].enabled = false;
		}
	}

	public void EnableColliders()
	{
		for (int i = 0; i < ColliderObjs.Length; i++)
		{
			ColliderObjs[i].enabled = true;
		}
	}
}
