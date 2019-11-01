using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBLA : MonoBehaviour
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
        StartCoroutine(waitSpawner());
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        n = maxnumber - 2;
        if (t > n)
        {
            stop = true;
        }
        else
        {
            stop = false;
        }
    }
    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {

            randEnemy = Random.Range(0, 1);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
            t = t + 1;
            Debug.Log("t=" + t);
            print("alan");
        }

    }
}