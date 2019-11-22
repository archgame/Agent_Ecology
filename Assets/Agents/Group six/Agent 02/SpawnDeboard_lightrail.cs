using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDeboard_lightrail : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randEnemy;
    public int t;
    int n;
    public int maxnumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stop = GetComponentInChildren<startSpawner_lightrail>().stopSpawn;

        if (!stop)
        {
            if (t < maxnumber)
            {
                randEnemy = Random.Range(0, 1);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
                Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
                for (float i = 0; i < spawnWait; i = i + Time.deltaTime)
                {
                    print(i);
                }
                //yield return new WaitForSeconds(spawnWait);
                t = t + 1;
                Debug.Log("t=" + t);
            }
        }

        else
        {
            t = 0;
        }
        
    }
}
