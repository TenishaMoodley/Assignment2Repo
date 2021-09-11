using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingMechanic : MonoBehaviour
{
    [Header("External Scripts")]
    ChangePlayer changeplayerSC;

    [Header("Unity Handles")]
    [SerializeField] LayerMask namelessPlayer;
    [SerializeField] LayerMask hidingSpot, hunterEnemy;
    [SerializeField] GameObject UI_Hint, TextCTRL;
    [SerializeField] Animator anim;
    [SerializeField] Color[] ShapesColor;
    [SerializeField] Transform Player, MoldCentre;

    [Header("Booleans")]
    public bool canHide;
    public bool currentlyHiding;

    [Header("Generic Elements")]
    [SerializeField] string nameOfObjectToHideIn;
    [SerializeField] string soundToPlayObject;

    [Header("Floats")]
    [SerializeField] float range;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        changeplayerSC = GetComponent<ChangePlayer>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("CurrentlyHiding", GameManager.ConfirmHiding);

        if (canHide && Input.GetKeyDown(KeyCode.LeftControl))
		{
            Physics.IgnoreLayerCollision(6, 7, true);
            currentlyHiding = true;
            //Hide Player and Play Animation/Camera Stuff/sound Here
           // if (GameManager.ConfirmHiding)
                MovePlayer();
            
            
        }
        /*if (changeplayerSC.checker==1)
        {
            TextCTRL.GetComponent<Text>().color = ShapesColor[0];
        }
        if (changeplayerSC.checker==2)
        {
            TextCTRL.GetComponent<Text>().color = ShapesColor[1];
        }
        if (changeplayerSC.checker==3)
        {
            TextCTRL.GetComponent<Text>().color = ShapesColor[2];
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
    public void ChangeColour(int index)
    {
        TextCTRL.GetComponent<Text>().color = ShapesColor[index-1];
    }
    void MovePlayer()
    {
        Player.transform.position = MoldCentre.position;
    }
    private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag(nameOfObjectToHideIn))
        {
            canHide = true;
            MoldCentre = other.GetComponentInParent<DisableCollider>().MoldCentre;
            ChangeColour(other.GetComponentInParent<DisableCollider>().ActualIndex);
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
