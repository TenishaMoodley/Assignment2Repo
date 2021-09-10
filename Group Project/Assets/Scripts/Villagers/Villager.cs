using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Villager : MonoBehaviour
{
    [Header("Unity Handles")]
    Rigidbody rb;
    NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask ground;
    [SerializeField] Vector3 walkingPoints;

    [Header("Booleans")]
    [SerializeField]bool walkingAlready;
    [SerializeField]bool talkingToPlayer;

    [Header("Floats")]
    [SerializeField] float walkingRanged;
    [SerializeField] float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!talkingToPlayer)
            Patrolling();
        if (talkingToPlayer)
            TalkToPlayer();
    }

    void Patrolling()
    {
        if (!walkingAlready)
            SearchForPath();

        if (walkingAlready)
        {
            Debug.Log("Walking");
           // rb.velocity = walkingPoints * speed;
            agent.SetDestination(walkingPoints);
            // Vector3.MoveTowards(transform.position, walkingPoints * speed, 1f);
            //rb.velocity = walkingPoints *
         }

        Vector3 dis = transform.position - walkingPoints;

        //Reached
        if (dis.magnitude < 1f)
            walkingAlready = false;
	}

    void SearchForPath()
	{
        float randomX = Random.Range(-walkingRanged, walkingRanged);
        float randomZ = Random.Range(-walkingRanged, walkingRanged);


        walkingPoints = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (walkingPoints != transform.position)
            walkingAlready = true;
    }
    void TalkToPlayer()
	{
        //Stop Villager from walking

        //Make villager look at player
        transform.LookAt(player);

        Debug.Log("Place dialogue here, like a thank you");
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player"))
            talkingToPlayer = true;
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            talkingToPlayer = false;
    }
}
