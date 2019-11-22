// This script moves the GameObject you attach in the Inspector to a Scene you specify in the Inspector.
// Attach this script to an empty GameObject.
// Click on the GameObject, go to its Inspector and type the name of the Scene you would like to load in the Scene field.
// Attach the GameObject you would like to move to a new Scene in the "My Game Object" field

// Make sure your Scenes are in your build (File>Build Settings).

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveGOToScene : MonoBehaviour
{
    // Type in the name of the Scene you would like to load in the Inspector
    public string m_Scene;

    // Assign your GameObject you want to move Scene in the Inspector
    public GameObject[] m_MyGameObjects;
    public GameObject InstantiateScript;

    void Update()
    {
        m_MyGameObjects = InstantiateScript.GetComponent<RandomTimeGenerate>().Scooters;
        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        foreach (GameObject Scooter in m_MyGameObjects)
        {
            SceneManager.MoveGameObjectToScene(Scooter, SceneManager.GetSceneByName(m_Scene));
        }
    }


}