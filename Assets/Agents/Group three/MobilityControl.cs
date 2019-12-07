using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilityControl : MonoBehaviour
{
    private string lastMobility = "";
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
        if (mobilityType == 2 && lastMobility == "Reset")
        {
            mobilityType = 0;
        }

        switch (mobilityType)
        {
            case 0:
                if((gameObject.GetComponent("ScooterRider") as ScooterRider) == null)
                {
                    gameObject.AddComponent<ScooterRider>();
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
            default:
                if((gameObject.GetComponent("BusRider") as BusRider) == null)
                {
                    lastMobility = "Reset";
                }
                break;

        }
    }
}
