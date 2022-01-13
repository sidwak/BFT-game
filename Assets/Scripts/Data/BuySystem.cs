using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySystem : MonoBehaviour
{
    public GameObject Machine1;
    public GameObject Machine2;
    public GameObject Machine2Buy;
    public GameObject Machine3;
    public GameObject Machine3Buy;
    public GameObject Machine4;
    public GameObject Machine4Buy;
    public GameObject Machine5;
    public GameObject Machine5Buy;
    public GameObject Machine6;
    public GameObject Machine6Buy;
    public GameObject Car2;
    public GameObject Car2Buy;
    public GameObject Car3;
    public GameObject Car3Buy;
    public GameObject Car4;
    public GameObject Car4Buy;
    public GameObject Car5;
    public GameObject Car5Buy;
    public GameObject Area2;
    public GameObject Area2Buy;
    public GameObject Area3;
    public GameObject Area3Buy;
    public GameObject Area4Buy;
    public GameObject Truck1;
    public GameObject Truck1Buy;
    public GameObject Truck2;
    public GameObject Truck2Buy;
    public GameObject Machine1Upgrade;
    public GameObject Machine2Upgrade;
    public GameObject Machine3Upgrade;
    public GameObject Machine4Upgrade;
    public GameObject Machine5Upgrade;
    public GameObject Machine6Upgrade;

    public JSONSave jsonSave;

    // Start is called before the first frame update
    void Start()
    {
        /*jsonSave.SavaData();
        jsonSave.LoadData();*/

        StartCoroutine(WaitForLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.5f);   //previous 1

        if (jsonSave.playerdata.Machine1Upgrade)
        {
            Machine1.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
            Destroy(Machine1Upgrade);
        }
        Machine2.SetActive(jsonSave.playerdata.Machine2Buy);
        Machine2Upgrade.SetActive(jsonSave.playerdata.Machine2Buy);
        if (jsonSave.playerdata.Machine2Buy)
        {
            Machine2Buy.SetActive(false);
            if (jsonSave.playerdata.Machine2Upgrade)
            {
                Machine2.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
                Destroy(Machine2Upgrade);
            }
        }
        Machine3.SetActive(jsonSave.playerdata.Machine3Buy);
        Machine3Upgrade.SetActive(jsonSave.playerdata.Machine3Buy);
        if (jsonSave.playerdata.Machine3Buy)
        {
            Machine3Buy.SetActive(false);
            if (jsonSave.playerdata.Machine3Upgrade)
            {
                Machine3.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
                Destroy(Machine3Upgrade);
            }
        }
        else
        {
            if (jsonSave.playerdata.Machine2Buy)
            {
                Machine3Buy.SetActive(true);
            }
        }
        Car2.SetActive(jsonSave.playerdata.Car2Buy);
        if (jsonSave.playerdata.Car2Buy)
        {
            Car2Buy.SetActive(false);
        }
        else
        {
            if (jsonSave.playerdata.Machine2Buy)
            {
                Car2Buy.SetActive(true);
            }
        }
        Car3.SetActive(jsonSave.playerdata.Car3Buy);
        if (jsonSave.playerdata.Car3Buy)
        {
            Car3Buy.SetActive(false);
        }
        else
        {
            if (jsonSave.playerdata.Machine2Buy)
            {
                Car3Buy.SetActive(true);
            }
        }
        Area2.SetActive(!jsonSave.playerdata.Area2Buy);
        if (jsonSave.playerdata.Area2Buy)
        {
            Area2Buy.SetActive(false);
        }
        else
        {
            /*if (jsonSave.playerdata.Car3Buy)
            {
                Area2Buy.SetActive(true);
            }*/
            if (jsonSave.playerdata.Machine2Buy)
            {
                Area2Buy.SetActive(true);
            }
        }
        Area3.SetActive(!jsonSave.playerdata.Area3Buy);
        if (jsonSave.playerdata.Area3Buy)
        {
            Area3Buy.SetActive(false);
            Area4Buy.SetActive(true);
        }
        else
        {
            if (jsonSave.playerdata.Area2Buy)
            {
                Area3Buy.SetActive(true);
            }
        }

        if (jsonSave.playerdata.Area2Buy)
        {
            Machine5.SetActive(jsonSave.playerdata.Machine5Buy);
            Machine5Upgrade.SetActive(jsonSave.playerdata.Machine5Buy);
            if (jsonSave.playerdata.Machine5Buy)
            {
                Machine5Buy.SetActive(false);
                if (jsonSave.playerdata.Machine5Upgrade)
                {
                    Machine5.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
                    Destroy(Machine5Upgrade);
                }
            }
            Car4.SetActive(jsonSave.playerdata.Car4Buy);
            if (jsonSave.playerdata.Car4Buy)
            {
                Car4Buy.SetActive(false);
            }
            Car5.SetActive(jsonSave.playerdata.Car5Buy);
            if (jsonSave.playerdata.Car5Buy)
            {
                Car5Buy.SetActive(false);
            }
        }
        else
        {
            Machine5Buy.SetActive(false);
            Car4Buy.SetActive(false);
            Car5Buy.SetActive(false);
        }
        if (jsonSave.playerdata.Area3Buy)
        {
            Machine4.SetActive(jsonSave.playerdata.Machine4Buy);
            Machine4Upgrade.SetActive(jsonSave.playerdata.Machine4Buy);
            if (jsonSave.playerdata.Machine4Buy)
            {
                Machine4Buy.SetActive(false);
                if (jsonSave.playerdata.Machine4Upgrade)
                {
                    Machine4.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
                    Destroy(Machine4Upgrade);
                }
            }
            Machine6.SetActive(jsonSave.playerdata.Machine6Buy);
            Machine6Upgrade.SetActive(jsonSave.playerdata.Machine6Buy);
            if (jsonSave.playerdata.Machine6Buy)
            {
                Machine6Buy.SetActive(false);
                if (jsonSave.playerdata.Machine6Upgrade)
                {
                    Machine6.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
                    Destroy(Machine6Upgrade);
                }
            }
            Truck1.SetActive(jsonSave.playerdata.Truck1Buy);
            if (jsonSave.playerdata.Truck1Buy)
            {
                Truck1Buy.SetActive(false);
            }
            Truck2.SetActive(jsonSave.playerdata.Truck2Buy);
            if (jsonSave.playerdata.Truck2Buy)
            {
                Truck2Buy.SetActive(false);
            }
        }
        else
        {
            Truck1Buy.SetActive(false);
            Truck2Buy.SetActive(false);
            Machine4Buy.SetActive(false);
            Machine6Buy.SetActive(false);
        }
    }

    public void WallBuy1()
    {
        Machine5Buy.SetActive(true);
        Car4Buy.SetActive(true);
        Car5Buy.SetActive(true);
    }
    public void WallBuy2()
    {
        Truck1Buy.SetActive(true);
        Truck2Buy.SetActive(true);
        Machine4Buy.SetActive(true);
        Area4Buy.SetActive(true);
    }

    public void BoughtMachine2()
    {
        Car2Buy.SetActive(true);
        Car3Buy.SetActive(true);
        Machine3Buy.SetActive(true);
        Area2Buy.SetActive(true);
    }

    public void BoughtCar3()
    {
        //Area2Buy.SetActive(true);
    }

    public void BoughtArea2()
    {
        Area3Buy.SetActive(true);
    }
}
