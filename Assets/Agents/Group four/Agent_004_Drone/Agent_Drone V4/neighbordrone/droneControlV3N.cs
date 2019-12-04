using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class droneControlV3N : MonoBehaviour
{
    //public float speed = 1.0F;

    public GameObject drone;

    [HideInInspector]
    public Vector3 startPos;
    private Vector3 endPos;

    public float lerpTime = 5;
    public float currentlerpTime = 0;


    private bool isColliding;


    //public float lerpTime = 5;
    //private float currentlerpTime = 0;
    //private bool keyHit = false;
    private float h;

    private Vector3 localdronestartPos;
    private Vector3 localdroneendPos;

    private Vector3 newlocaldroneendPos;

    private bool pickUp = false;
    //private bool isHeigh;

    // Start is called before the first frame update


    public bool getGoods = false;


    void Start()
    {

        //Vector3 startPos = GetComponent<droneControlV2>().localdronepoint;
        //Vector3 localdronestartPos = drone.transform.position;
        //Vector3 localdronepoint = new Vector3(startPos.x, startPos.y, startPos.z);

        

    }

    // Update is called once per frame
    void Update()
    {


        h = GetComponent<droneControlV2N>().drone.transform.position.y;
        

        //Debug.Log("H " + drone.transform.position);
        //Debug.Log("Hlocal " + transform.position);

        Vector3 localdronestartPos = new Vector3(drone.transform.position.x,
            drone.transform.position.y,
            drone.transform.position.z);
        Vector3 localdroneendPos = new Vector3(drone.transform.position.x,
               drone.transform.position.y - h,
               drone.transform.position.z);

        Vector3 newlocaldroneendPos = GetComponent<droneControlV2N>().newlocaltargetpoint;
        //Vector3 newlocaldroneendPos = new Vector3(GetComponent<droneControlV2>().transform.position.x,
        //      GetComponent<droneControlV2>().transform.position.y - h,
        //      GetComponent<droneControlV2>().transform.position.z);

        Debug.Log("newlocaldroneendPos " + newlocaldroneendPos);

        //Debug.Log("localdronestartPos " + localdronestartPos);
        //Debug.Log("localdroneendPos " + localdroneendPos);

        currentlerpTime += Time.deltaTime;


        if (currentlerpTime >= lerpTime)
        {
            currentlerpTime = lerpTime;
        }
        float perc = currentlerpTime / lerpTime / 50;

        //float step = speed * Time.deltaTime;
        drone.transform.position = Vector3.MoveTowards(localdronestartPos, newlocaldroneendPos, perc);
        GetComponent<Collider>().isTrigger = true;
        //Debug.Log("localdronestartPos " + localdronestartPos);
        //Debug.Log("localdroneendPo " + localdroneendPos);




        if (drone.transform.position.y <= 3)
        
        {
            GetComponent<Collider>().isTrigger = true;
            
        }
        


        if (GetComponent<droneControlV2N>().finishTour == true && drone.transform.position.y <= 0.1f)
        {
            //gameObject.GetComponent<droneControlV1>().enabled = true;



            GetComponent<droneControlV2N>().enabled = false;
            GetComponent<droneControlV2N>().finishTour = false;
            

            GetComponent<droneControlV1N>().enabled = true;
            GetComponent<droneControlV1N>().currentlerpTime = 0;

            
            //GetComponent<droneControlV3>().getGoods = true;
            GetComponent<droneControlV3N>().enabled = false;

        }


    }
    void OnTriggerEnter(Collider goods)
    {
        //bird = collider.transform.parent;
        //collider.transform.parent = people.transform;

        


        //if (goods.gameObject.CompareTag("Target") && getGoods==true)
        //{
        //    foreach (Transform child in transform)
        //   {
        //        if (child.name == "goods")
        //       {
        //            
        //            transform.parent = null;
        //        }
        //    }
        //}


       

        if (goods.gameObject.CompareTag("goods") || transform.childCount == 1  )
        {
            //print("item pick up");
            //if (transform.childCount == 0 && getGoods == false)
            if (transform.childCount == 0 && !getGoods)

            {

                //print(getGoods);
                pickUp = drone.transform.parent;
                goods.transform.parent = drone.transform;
                goods.transform.forward = drone.transform.forward;
                goods.transform.position = drone.transform.position;
                getGoods = true;
            }
            
            else if(goods.gameObject.CompareTag("dronetarget2") && getGoods)
            {
                
                transform.DetachChildren();
                getGoods = false;
                //print("1");
            }



        }


    }


}
