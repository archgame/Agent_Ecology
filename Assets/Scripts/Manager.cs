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
        /*if (Input.GetKeyUp("2"))
        {
            CheckScene("Team_002");
        }*/
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

        if (Input.GetKeyUp("0"))
        {
            SceneManager.LoadScene("Team_001", LoadSceneMode.Additive);
            //SceneManager.LoadScene("Team_002", LoadSceneMode.Additive);
            SceneManager.LoadScene("Team_003", LoadSceneMode.Additive);
            SceneManager.LoadScene("Team_004", LoadSceneMode.Additive);
            SceneManager.LoadScene("Team_005", LoadSceneMode.Additive);
            SceneManager.LoadScene("Team_006", LoadSceneMode.Additive);
        }
        if(Input.GetKeyUp("-"))
        {
            SceneManager.LoadScene("Agent Ecology", LoadSceneMode.Single);
            //SafeUnload("Team_001");
            //SafeUnload("Team_002");
            //SafeUnload("Team_003");
            //SafeUnload("Team_004");
            //SafeUnload("Team_005");
            //SafeUnload("Team_006");
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

    void SafeUnload(string name)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.name == name)
            {
                SceneManager.UnloadSceneAsync(name);
            }
        }
    }
}
