using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Unity Handles")]
    public GameObject[] prefab;

    [Header("Generic Elements")]
    [SerializeField] Queue<GameObject> prefabPool = new Queue<GameObject>();

    [Header("Integers")]
    public int poolSizeStart = 5;

    void Start()
    {
        int random = Random.Range(0, prefab.Length);

        for (int i = 0; i < poolSizeStart; i++)
		{
            GameObject obj = Instantiate(prefab[random]);
            prefabPool.Enqueue(obj);
            obj.SetActive(false);
		}
    }

   public GameObject GetPrefab()
	{
        int random = Random.Range(0, prefab.Length);
        
        if (prefabPool.Count > 0)
		{
            GameObject obj = prefabPool.Dequeue();
            obj.SetActive(true);
            return obj;
		}
        else
		{
            GameObject fab = Instantiate(prefab[random]);
            return fab;
		}
	}

    public void ReturnPrefab(GameObject obj)
	{
        prefabPool.Enqueue(obj);
        obj.SetActive(false);
	}
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/


