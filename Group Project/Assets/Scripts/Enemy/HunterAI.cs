using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class HunterAI : MonoBehaviour
{
    #region Variables
    public enum typeOfEnemy { walkEverywhere, straight}

    [Header("States")]
    [SerializeField] typeOfEnemy typeOfHunter;

    [Header("External Scripts")]
    DashEnemy enemyDash;
    HidingMechanic hm;
    Pickup[] pick = new Pickup[3];

    [Header("Unity Handles")]
    [SerializeField] NavMeshAgent hunterAgent;
    public Transform namelessPlayer;
    [SerializeField] Transform[] spotsToPatrol;
    [SerializeField] ChangePlayer changePlayerScript;
    [SerializeField] LayerMask ground, player, environmment;
    [SerializeField] Vector3 walkingPoint;
    [SerializeField] GameObject[] pickUp = new GameObject[3];

    [Header("Booleans")]
    public static bool hasTakenColour;
    [SerializeField] bool walkingSet;
    [SerializeField] bool canTakeColour;
    public bool playerInSight;

    [Header("Floats")]
    [SerializeField] float walkSetRange;
    [SerializeField] float startingWalkRange, walkingSetRange;
    [SerializeField] float timeBetweenTakingColour;
    [SerializeField] float takeColourRange, defaultNavSpeed, defaultAcceleration;

    [Header("Integers")]
    [SerializeField] int randomPosition;
    [SerializeField] int lastRandomPos;
    [SerializeField] int twoPointsPos;

    [Header("Generic Elements")]
    [SerializeField] string[] shapeTriggered = new string[3];
    [Header("Picking Up Variables")]
   // [SerializeField] int ind

    [Range(0,360f)]
    public float angle;

    [Range(0, 360f)]
    public float sightRange;

	#endregion
	private void Awake()
	{
        namelessPlayer = GameObject.FindWithTag("Player").transform;
        hunterAgent = GetComponent<NavMeshAgent>();
        enemyDash = GetComponent<DashEnemy>();
        changePlayerScript = FindObjectOfType<ChangePlayer>();
        hm = FindObjectOfType<HidingMechanic>();
        pickUp = GameObject.FindGameObjectsWithTag("Pickup");
		
        for (int i = 0; i < pickUp.Length; i++)
		{
            pick[i] = pickUp[i].GetComponent<Pickup>();
		}
	}

    void Start()
	{
        walkingSetRange = startingWalkRange;
        randomPosition = Random.Range(0, spotsToPatrol.Length);

        defaultNavSpeed = hunterAgent.speed;
        defaultAcceleration = hunterAgent.acceleration;

        if (typeOfHunter == typeOfEnemy.straight)
		{
            walkingPoint = spotsToPatrol[twoPointsPos].position;
            hunterAgent.SetDestination(walkingPoint);
        }
	}

    void Update()
    {
       //playerInSight = Physics.CheckSphere(transform.position, sightRange, player);
       canTakeColour = Physics.CheckSphere(transform.position, takeColourRange, player);

        //Ccheccck
        AngleSight();

        //Check the states
        if (!playerInSight && !canTakeColour)
            StandAround();
        if (playerInSight && !canTakeColour && !hm.currentlyHiding)
            ChasePlayer();
        if (playerInSight && canTakeColour && hasColours() && !hm.currentlyHiding)
            TakeColour();

    }


	#region States
	void StandAround()
    {
        hunterAgent.speed = defaultNavSpeed;
        hunterAgent.acceleration = defaultAcceleration;

        if (typeOfHunter == typeOfEnemy.straight)
            SearchForPointsToStandAround();

        if (typeOfHunter == typeOfEnemy.walkEverywhere)
        {
            if (spotsToPatrol != null)
                hunterAgent.SetDestination(spotsToPatrol[randomPosition].position);

            //Distance to standing point
            Vector3 disToStandPoint = transform.position - spotsToPatrol[randomPosition].position;

            if (lastRandomPos != randomPosition)
            {
                //Point Reached
                if (disToStandPoint.magnitude < 1f)
                {
                    //walkingSet = false;
                    if (walkingSetRange <= 0)
                    {
                        //Debug.Log("Change Route");
                        randomPosition = Random.Range(0, spotsToPatrol.Length);
                        lastRandomPos = randomPosition;

                        walkingPoint = spotsToPatrol[randomPosition].position;
                       
                        walkingSetRange = startingWalkRange;
                    }
                    else
                        walkingSetRange -= Time.deltaTime;
                }
                
            }
            if (lastRandomPos == randomPosition)
                randomPosition = Random.Range(0, spotsToPatrol.Length);
        }
    }


    void ChasePlayer()
    {
        Debug.Log("Chasing");
      StartCoroutine(enemyDash.Dash(hunterAgent, namelessPlayer));

    }

    void TakeColour()
    {
        Debug.Log("Take COlour");
        hunterAgent.SetDestination(transform.position);
        transform.LookAt(namelessPlayer);

        if(!hasTakenColour)
		{
            //Hunter Will Take Colour if player has colour
           if(changePlayerScript.hasColour[1] && PlayerPrefs.GetInt(shapeTriggered[0]) != 1)
			{
                Debug.Log("Colour: " + pickUp[0].name + " Taken");
                //Play Animation /Particles

                pickUp[0].SetActive(true);
                pickUp[0].transform.parent.gameObject.SetActive(true);

                //Remove COlour from player (We should Lerp this)
                pick[0].isColour = false;
                changePlayerScript.ReturnShape();
                changePlayerScript.hasColour[1] = pick[0].isColour;
                changePlayerScript.hasShapeColour[0] = pick[0].isColour;

                //Play Sound
                FindObjectOfType<MusicManager>().Play("LostColour");
            }
            if (changePlayerScript.hasColour[2] && PlayerPrefs.GetInt(shapeTriggered[1]) != 1)
            {
                Debug.Log("Colour: " + pickUp[1].name + " Taken");
                //Play Animation /Particles

                pickUp[1].SetActive(true);
                pickUp[1].transform.parent.gameObject.SetActive(true);

                //Remove COlour from player (We should Lerp this)
                pick[1].isColour = false;
                changePlayerScript.ReturnShape();
                changePlayerScript.hasColour[2] = pick[1].isColour;
                changePlayerScript.hasShapeColour[1] = pick[1].isColour;

                //Play Sound
            }
            if (changePlayerScript.hasColour[3] && PlayerPrefs.GetInt(shapeTriggered[2]) != 1)
            {
                Debug.Log("Colour: " + pickUp[2].name + " Taken");
                //Play Animation /Particles

                pickUp[2].SetActive(true);
                pickUp[2].transform.parent.gameObject.SetActive(true);

                //Remove COlour from player (We should Lerp this)
                pick[2].isColour = false;
                changePlayerScript.ReturnShape();
                changePlayerScript.hasColour[3] = pick[2].isColour;
                changePlayerScript.hasShapeColour[2] = pick[2].isColour;
                //Play Sound
            }

            //Figure out Picking Up Boolean
            hasTakenColour = true;
            Invoke(nameof(ResetTakingColour), timeBetweenTakingColour);
		}
    }
    #endregion

    void SearchForPointsToStandAround()
	{
       // Vector3 disToStandPoint = transform.position - walkingPoint;
        hunterAgent.SetDestination(walkingPoint);

        if (Vector2.Distance(transform.position, walkingPoint) <= 1f)
        {
            if (walkingSetRange <= 0)
            {
                Debug.Log("Change Route");
                twoPointsPos++;

                walkingPoint = spotsToPatrol[twoPointsPos].position;
                
                walkingSetRange = startingWalkRange;

                if (twoPointsPos == 0)
                    twoPointsPos = 0;
                else if (twoPointsPos == 1)
                    twoPointsPos = -1;
            }
            else
                walkingSetRange -= Time.deltaTime;

        }
      
    }

    void AngleSight()
	{
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightRange, player);

        if (rangeChecks.Length != 0)
        {
            Transform playerTar = rangeChecks[0].transform;
            Vector3 dir = (playerTar.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) < angle / 2)
            {
                float dis = Vector3.Distance(transform.position, playerTar.position);

                if (!Physics.Raycast(transform.position, dir, dis, environmment))
                {
                    playerInSight = true;
                }
                else
                    playerInSight = false;
            }
            else
                playerInSight = false;
        }
        else if (playerInSight)
            playerInSight = false;
	}

    public void ResetTakingColour()
	{
        hasTakenColour = false;
	}


    public bool hasColours()
	{
        if (changePlayerScript.hasColour[0] || changePlayerScript.hasColour[1] || changePlayerScript.hasColour[2])
            return true;
        else
            return false;
	}
	//Visualising
	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, takeColourRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
	}
}

