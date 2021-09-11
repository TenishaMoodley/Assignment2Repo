using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
	[Header("States")]
	public typeOfCollider col;
	public enum typeOfCollider { cube, sphere, prism}
	[Header("Unity Handles")]
	[SerializeField] Collider[] ColliderObjs;
    public Transform  MoldCentre, GetOut;
    

	[Header("Externa Scripts")]
	HidingMechanic hm;
	ChangePlayer changePlayerScript;

	[Header("Index")]
	[SerializeField] int shapeIndex;
    public int ActualIndex;

	private void Start()
	{
		hm = FindObjectOfType<HidingMechanic>();
		changePlayerScript = FindObjectOfType<ChangePlayer>();
	}
	private void Update()
    {
        if (hm.canHide)
		{
			if (hm.checkedBool && !hm.canPlayCamAnim)
			{
				Disbable();
				hm.MovePlayer();
			}
		}
		else
		{
			EnableColliders();
        }
	}
  
    #region ColiderStuff
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
    #endregion
}
