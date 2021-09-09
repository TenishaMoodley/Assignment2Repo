using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [Header("Unity Handles")]
    [SerializeField] GameObject hunterEnemy;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform parentForHunter;

    [Header("Floats")]
    [SerializeField] float mySpawns;
    [SerializeField] float startTime, enemySpawned, totalEnemies;
    [SerializeField] float decrTime, minTime = 0.65f;

    private void Start()
	{
        
        
	}
    void Update()
    {
        if (mySpawns <= 0 && enemySpawned <= totalEnemies)
        {
            //int random = Random.Range(0, enemyPatterns.Length);
            GameObject fab = Instantiate(hunterEnemy, spawnPoint.position, Quaternion.identity);
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
