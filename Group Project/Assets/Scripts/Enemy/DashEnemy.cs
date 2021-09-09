using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    [Header("Floats")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDistance = 8f, multiplier = 2f;

    public IEnumerator Dash(NavMeshAgent agent, Transform player)
	{
        yield return new WaitForSeconds(0.4f);

        agent.speed = dashSpeed + dashDistance;
        agent.acceleration += (multiplier * Time.deltaTime);
        agent.acceleration = Mathf.Clamp(agent.acceleration, 0, 35f);
        agent.transform.LookAt(player);
        agent.SetDestination(player.position);
	}
}
