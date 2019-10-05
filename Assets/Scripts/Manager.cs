using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("1"))
        {
            CheckScene("Team_001");
        }
        if (Input.GetKeyUp("2"))
        {
            CheckScene("Team_002");
        }
        if (Input.GetKeyUp("3"))
        {
            CheckScene("Team_003");
        }
        if (Input.GetKeyUp("4"))
        {
            CheckScene("Team_004");
        }
        if (Input.GetKeyUp("5"))
        {
            CheckScene("Team_005");
        }
        if (Input.GetKeyUp("6"))
        {
            CheckScene("Team_006");
        }
    }

    void CheckScene(string name)
    {
        bool alreadyLoaded = false;
        for(int i = 0;i<SceneManager.sceneCount;i++)
        {
            Scene s = SceneManager.GetSceneAt(i);
            if(s.name == name)
            {
                alreadyLoaded = true;
            }
        }

        if(!alreadyLoaded)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync(name);
        }
        
    }
}
