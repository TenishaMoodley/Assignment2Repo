using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [Header("Unity Handles")]
    [SerializeField] GameObject[] hunterEnemy;
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] Transform parentForHunter;

    [Header("Floats")]
    [SerializeField] float mySpawns;
    [SerializeField] float startTime, enemySpawned, totalEnemies;
    [SerializeField] float decrTime, minTime = 0.65f;

    [Header("Integers")]
    int randomSpot;

    [Header("Booleans")]
    bool allSpawned;

	private void OnEnable()
	{
        allSpawned = false;
    }
	private void Update()
	{
        if (!allSpawned)
        {
            randomSpot = Random.Range(0, spawnPoint.Length);
            for (int i = 0; i < hunterEnemy.Length; i++)
            {
                if (mySpawns <= 0 && enemySpawned <= totalEnemies)
                {
                    randomSpot = Random.Range(0, spawnPoint.Length);
                    //int random = Random.Range(0, enemyPatterns.Length);
                    GameObject fab = Instantiate(hunterEnemy[i], spawnPoint[randomSpot].position, Quaternion.identity);
                    fab.transform.SetParent(parentForHunter);
                    enemySpawned++;
                    mySpawns = startTime;
                    if (startTime > minTime)
                        startTime -= decrTime;
                }
                else
                {
                    mySpawns -= Time.deltaTime;
                }

            }
        }

        if (enemySpawned >= totalEnemies)
            allSpawned = true;
    }
}
