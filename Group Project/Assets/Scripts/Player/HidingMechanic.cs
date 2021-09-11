using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingMechanic : MonoBehaviour
{
    [Header("External Scripts")]
    ChangePlayer changeplayerSC;
    CharcterControllerTPV controller;

    [Header("Unity Handles")]
    [SerializeField] LayerMask namelessPlayer;
    [SerializeField] LayerMask hidingSpot, hunterEnemy;
    [SerializeField] GameObject UI_Hint, TextCTRL;
    [SerializeField] Animator anim;
    [SerializeField] Color[] ShapesColor;
    [SerializeField] Transform Player, MoldCentre, GetOut;

    [Header("Booleans")]
    public bool canHide;
    public bool currentlyHiding, canPlayCamAnim, checkedBool, insideMold;

    [Header("Generic Elements")]
    [SerializeField] string nameOfObjectToHideIn;
    [SerializeField] string soundToPlayObject;
    [SerializeField] string defaultHiding, stopHiding;

    [Header("Floats")]
    [SerializeField] float range;

	private void Awake()
	{
        defaultHiding = UI_Hint.GetComponent<Text>().text;

        UI_Hint.SetActive(false);

    }
	private void Start()
    {
        changeplayerSC = GetComponent<ChangePlayer>();
        controller = GetComponent<CharcterControllerTPV>();

        canPlayCamAnim = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Camera Animation
        anim.SetBool("CurrentlyHiding", canPlayCamAnim);

        if (currentlyHiding && Input.GetKeyDown(KeyCode.LeftControl))
		{
            Physics.IgnoreLayerCollision(6, 7, true);
            checkedBool = true;
        }

        if (insideMold && Input.GetKeyDown(KeyCode.LeftControl))
        {
            canPlayCamAnim = false;
            Player.transform.position = GetOut.position;

            controller.canMove = true;
            currentlyHiding = false;
            checkedBool = false;
            Physics.IgnoreLayerCollision(6, 7, false);
        }

        //ChangeText
        if (insideMold)
            UI_Hint.GetComponent<Text>().text = stopHiding;
    }

    public void ChangeColour(int index)
    {
        TextCTRL.GetComponent<Text>().color = ShapesColor[index-1];
    }
    public void MovePlayer()
    {
        Player.transform.position = MoldCentre.position;
        Debug.Log("Called");

        StartCoroutine(ActivateAnim());
    }

	#region Triggers
	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag(nameOfObjectToHideIn))
        {
            canHide = true;
            MoldCentre = other.GetComponentInParent<DisableCollider>().MoldCentre;
            ChangeColour(other.GetComponentInParent<DisableCollider>().ActualIndex);
            Check(other.GetComponentInParent<DisableCollider>().ActualIndex);

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(nameOfObjectToHideIn))
        { 
            Check(other.GetComponentInParent<DisableCollider>().ActualIndex);
            GetOut = other.GetComponentInParent<DisableCollider>().GetOut;
        }
        if(other.CompareTag(soundToPlayObject))
		{
            insideMold = true;
		}
    }
	private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(nameOfObjectToHideIn))
        { 
            canHide = false;
            Debug.Log("Can't Hide!");
            currentlyHiding = false;
            UI_Hint.SetActive(false);
            anim.SetBool("CurrentlyHiding", currentlyHiding);
        }
        if(other.CompareTag(soundToPlayObject))
		{
            UI_Hint.GetComponent<Text>().text = defaultHiding;
            UI_Hint.SetActive(false);
            insideMold = false;
		}
    }
#endregion
    void Check(int index)
    {
        if (changeplayerSC.isShape[index])
        {
            currentlyHiding = true;
        }
        else if (changeplayerSC.isShape[index])
        {
            currentlyHiding = true;
        }
        else if (changeplayerSC.isShape[index])
        {
            currentlyHiding = true;
        }
    }

    IEnumerator ActivateAnim()
	{
        yield return new WaitForSeconds(.5f);

        canPlayCamAnim = true;
        controller.canMove = false;
	}
	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
	}

}
