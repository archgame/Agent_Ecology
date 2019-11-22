using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneControl2 : MonoBehaviour
{
    public float speed = 10.0F;
    

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.GetComponent<droneControl1>().getheight == true)
        {
            gameObject.GetComponent<droneControl1>().enabled = false;

            float translation = Input.GetAxis("Vertical") * speed;
            float straff = Input.GetAxis("Horizontal") * speed;

            translation *= Time.deltaTime;
            straff *= Time.deltaTime;


            transform.Translate(straff, 0, translation);
            //if (Input.GetKeyDown("escape"))
            //{
            //    Cursor.lockState = CursorLockMode.None;
            //}
        }

    }

}
