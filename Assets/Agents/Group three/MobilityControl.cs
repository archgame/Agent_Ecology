using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilityControl : MonoBehaviour
{
    public string lastMobility = "";
    public int mobilityType = 0;


    // Update is called once per frame
    void Update()
    {

        //make sure last mobility wasn't current mobililty
        if (mobilityType == 0 && lastMobility == "Scooter")
        {
            mobilityType++;
            Destroy(GetComponent<ScooterRider>());
        }
        if (mobilityType == 1 && lastMobility == "Bus")
        {
            mobilityType++;
        }
        if (mobilityType == 2 && lastMobility == "IceCreamTruck")
        {
            mobilityType++;
            if ((gameObject.GetComponent("IceCreamQueue") as IceCreamQueue) != null)
            {
                gameObject.GetComponent<IceCreamQueue>().enabled = false;
                lastMobility = "";
            }

        }
        if (mobilityType == 3 && lastMobility == "Reset")
        {
            mobilityType = 0;

        }

        switch (mobilityType)
        {
            case 0:
                if((gameObject.GetComponent("ScooterRider") as ScooterRider) == null)
                {
                    gameObject.AddComponent<ScooterRider>();
                    gameObject.GetComponent<MeshRenderer>().materials[0].color = new Color(0f / 255f, 255f / 255f, 175f / 255f); ;
                }
                lastMobility = gameObject.GetComponent<ScooterRider>().scooter();
                break;
            case 1:
                if ((gameObject.GetComponent("BusRider") as BusRider) == null)
                {
                    gameObject.AddComponent<BusRider>();
                }
                lastMobility = gameObject.GetComponent<BusRider>().bus();
                break;
            case 2:
                if ((gameObject.GetComponent("IceCreamQueue") as IceCreamQueue) != null&& (gameObject.GetComponent("BusRider") as BusRider) == null)
                {
                    gameObject.GetComponent<IceCreamQueue>().enabled =true;
                    gameObject.GetComponent<MeshRenderer>().materials[0].color = new Color(255f / 255f, 112f / 255f, 0f / 255f); ;

                }
                if ((gameObject.GetComponent("IceCreamQueue") as IceCreamQueue) != null)
                {
                    bool finished = GetComponent<IceCreamQueue>().finished;
                    lastMobility = gameObject.GetComponent<IceCreamQueue>().Icecream(finished, lastMobility);
                }
                else
                {
                    lastMobility = "IceCreamTruck";
                }
                break;
            default:
                if((gameObject.GetComponent("BusRider") as BusRider) == null)
                {
                    lastMobility = "Reset";

                }
                break;
        }
    }

    float waited = 0;
    public bool Switch(bool ifswitchLights, float time)
    {
        //float waited = 0;
        float waitTime = time;
        if (waited > waitTime)
        {
            waited = 0;
            ifswitchLights = !ifswitchLights;
        }
        else
        {
            waited += Time.deltaTime * 3;
            //Debug.Log(waited);
            //Debug.Log("if " + ifswitchLights);

        }
        return ifswitchLights;
    }
}
