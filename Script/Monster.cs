using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private List<GameObject> mums = new List<GameObject>();
    public float spawnTime = 3f;
    private Vector3 spawnPosition;
    private GameObject rab;


    private void Awake()
    {
        rab = Resources.Load<GameObject>("Prefabs/Monster/Rabid");
        
    }
    private void Start()
    {
        CreateMum();
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
    private void CreateMum()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject enemy = Instantiate(rab);
            enemy.SetActive(false);
            mums.Add(enemy);
        }
    }

    void Spawn()
    {
        foreach (GameObject obj in mums)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                spawnPosition.x = Random.Range(0, 5);
                spawnPosition.y = 0.0f;
                spawnPosition.z = Random.Range(-30, -35);
                obj.transform.position = spawnPosition;
                break;
            }
        }


    }
}
