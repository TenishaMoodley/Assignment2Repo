using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingMechanic : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] LayerMask namelessPlayer;
    [SerializeField] LayerMask hidingSpot, hunterEnemy;
    [SerializeField] GameObject UI_Hint;
    [SerializeField] Animator anim;
    

    [Header("Booleans")]
    [SerializeField] bool canHide;
    public bool currentlyHiding;

    [Header("Generic Elements")]
    [SerializeField] string nameOfObjectToHideIn;
    [SerializeField] string soundToPlayObject;

    [Header("Floats")]
    [SerializeField] float range;

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("CurrentlyHiding", currentlyHiding);

        if (canHide && Input.GetKeyDown(KeyCode.LeftControl))
		{
            Physics.IgnoreLayerCollision(6, 7, true);

            //Hide Player and Play Animation/Camera Stuff/sound Here
            
            currentlyHiding = true;
            
        }
      /*  else
		{
            Physics.IgnoreLayerCollision(6, 7, false);

            //Player Stops Hiding and Play Animation/sound Here

            currentlyHiding = false;
        }*/


    }

	private void FixedUpdate()
	{
        if (!currentlyHiding)
            Debug.Log("Player Moves");
        else
            Debug.Log("StopPlayerMovement" + " Is Hiding");
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag(nameOfObjectToHideIn))
        {
            canHide = true;

            //Play Sound
            FindObjectOfType<MusicManager>().Play("Notification");

            UI_Hint.SetActive(canHide);
            Debug.Log("Can Hide!");
        }

        if (other.CompareTag(soundToPlayObject))
        {
            //Sound for entering mold
            FindObjectOfType<MusicManager>().Play("Hiding");
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(nameOfObjectToHideIn))
        { 
            canHide = false;
            UI_Hint.SetActive(canHide);
            Debug.Log("Can't Hide!");
            currentlyHiding = false;
            anim.SetBool("CurrentlyHiding", currentlyHiding);
        }
    }

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
	}
}
