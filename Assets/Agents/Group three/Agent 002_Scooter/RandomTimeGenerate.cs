using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class RandomTimeGenerate : MonoBehaviour
{
    public GameObject Scooter;
    public int ScooterCount;
    public GameObject[] InstantiateTarget;
    public GameObject[] Scooters;




    // Start is called before the first frame update
    void Start()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

        List<GameObject> targetList = new List<GameObject>();
        foreach (GameObject go in targets) //search all "Target" game objects
        {
            if (go.name.Contains("Scooter"))
            {
                targetList.Add(go);
            }
        }
        InstantiateTarget = targetList.ToArray(); //Convert List to Array, because other code is still using array


        //Instantiate Scooter
        List<GameObject> scooterList = new List<GameObject>();

        for (int i = 0; i < ScooterCount; i++)
        {
            GameObject obj = Instantiate(Scooter);
            int a = i % InstantiateTarget.Length;
            obj.transform.position = InstantiateTarget[a].transform.position;
            obj.SetActive(false);
            scooterList.Add(obj);
        }
        Scooters = scooterList.ToArray();
        Scooters = Shuffle(Scooters);

    }

    // Update is called once per frame
    void Update()
    {
        float time = 2f;
        for (int i = 0; i < Scooters.Length; i++)
        {

            time -= Time.deltaTime*2f;
            //Debug.Log(time);
            if(time < 0)
            {
                //Debug.Log("yeah");
                time = Random.Range(2,2.5f);
                Scooters[i].SetActive(true);
            }
        }
    }


    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGO;
        for (int i = 0; i < objects.Length; i++)
        {
            //Debug.Log("i: " + i);
            int rnd = Random.Range(0, objects.Length);
            tempGO = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempGO;

        }
        return objects;
    }
}
