using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuySystem : MonoBehaviour
{
    public int levelCur;
    public int nextLevel = 1;

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
    public GameObject Machine7;
    public GameObject Machine7Buy;
    public GameObject Car2;
    public GameObject Car2Buy;
    public GameObject Car3;
    public GameObject Car3Buy;
    public GameObject Car4;
    public GameObject Car4Buy;
    public GameObject Car5;
    public GameObject Car5Buy;
    public GameObject Car6;
    public GameObject Car6Buy;
    public GameObject Area2;
    public GameObject Area2Buy;
    public GameObject Area3;
    public GameObject Area3Buy;
    public GameObject Area4;
    public GameObject Area4Buy;
    public GameObject Area5;
    public GameObject Area5Buy;
    public GameObject Truck1;
    public GameObject Truck1Buy;
    public GameObject Truck2;
    public GameObject Truck2Buy;

    public GameObject Rack2;
    public GameObject Rack2Buy;
    public GameObject Rack3;
    public GameObject Rack3Buy;
    public GameObject Rack4;
    public GameObject Rack4Buy;

    public GameObject Machine1Upgrade;
    public GameObject Machine2Upgrade;
    public GameObject Machine3Upgrade;
    public GameObject Machine4Upgrade;
    public GameObject Machine5Upgrade;
    public GameObject Machine6Upgrade;
    public GameObject Machine7Upgrade;

    public GameObject buyers;
    public GameObject Level2Buy;

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
            //Area4Buy.SetActive(true);
        }
        else
        {
            if (jsonSave.playerdata.Area2Buy)
            {
                Area3Buy.SetActive(true);
            }
        }
        Area4.SetActive(!jsonSave.playerdata.Area4Buy);
        if (jsonSave.playerdata.Area4Buy)
        {
            Area4Buy.SetActive(false);
        }
        else
        {
            if (jsonSave.playerdata.Area3Buy)
            {
                Area4Buy.SetActive(true);
            }
        }
        Area5.SetActive(!jsonSave.playerdata.Area5Buy);
        if (jsonSave.playerdata.Area5Buy)
        {
            Area5Buy.SetActive(false);
        }
        else
        {
            if (jsonSave.playerdata.Area4Buy)
            {
                Area5Buy.SetActive(true);
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
            buyers.SetActive(true);
            Rack2.SetActive(jsonSave.playerdata.Rack2Buy);
            if (jsonSave.playerdata.Rack2Buy)
            {
                Rack2Buy.SetActive(false);
            }
            Rack3.SetActive(jsonSave.playerdata.Rack3Buy);
            if (jsonSave.playerdata.Rack3Buy)
            {
                Rack3Buy.SetActive(false);
            }
            Rack4.SetActive(jsonSave.playerdata.Rack4Buy);
            if (jsonSave.playerdata.Rack4Buy)
            {
                Rack4Buy.SetActive(false);
            }
        }
        else
        {
            Rack2Buy.SetActive(false);
            Rack3Buy.SetActive(false);
            Rack4Buy.SetActive(false);
        }
        if (jsonSave.playerdata.Area4Buy)
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
        if (jsonSave.playerdata.Area5Buy)
        {
            Machine7.SetActive(jsonSave.playerdata.Machine7Buy);
            Machine7Upgrade.SetActive(jsonSave.playerdata.Machine7Buy);
            if (jsonSave.playerdata.Machine7Buy)
            {
                Machine7Buy.SetActive(false);
                if (jsonSave.playerdata.Machine7Upgrade)
                {
                    Machine7.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
                    Destroy(Machine7Upgrade);
                }
            }
            Car6.SetActive(jsonSave.playerdata.Car6Buy);
            if (jsonSave.playerdata.Car6Buy)
            {
                Car6Buy.SetActive(false);
            }
            if (jsonSave.playerdata.curLevel == levelCur)
            {
                Level2Buy.SetActive(true);
            }
        }
        else
        {
            Machine7Buy.SetActive(false);
            Car6Buy.SetActive(false);
            Level2Buy.SetActive(false);
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
        buyers.SetActive(true);
        Rack2Buy.SetActive(true);
        Rack3Buy.SetActive(true);
        Rack4Buy.SetActive(true);
        Area4Buy.SetActive(true);
    }

    public void WallBuy3()
    {
        Truck1Buy.SetActive(true);
        Truck2Buy.SetActive(true);
        Machine4Buy.SetActive(true);
        Machine6Buy.SetActive(true);
        Area5Buy.SetActive(true);
    }

    public void WallBuy4()
    {
        Level2Buy.SetActive(true);
        Machine7Buy.SetActive(true);
        Car6Buy.SetActive(true);
    }

    public void NextLevel()
    {
        Debug.Log(nextLevel);
        SceneManager.LoadScene(nextLevel);
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
