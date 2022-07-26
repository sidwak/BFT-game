using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuySystem : MonoBehaviour
{
    public int levelCur;
    public int nextLevel = 1;

    public GameObject Machine1;
    public GameObject Machine1Buy;
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
    public GameObject Car1;
    public GameObject Car1Buy;
    public GameObject Car2;
    public GameObject Car2Buy;
    public GameObject Car3;
    public GameObject Car3Buy;
    public GameObject Car4;
    public GameObject Car4Buy;
    //public GameObject Car5;
    //public GameObject Car5Buy;
    public GameObject Car6;
    public GameObject Car6Buy;
    public GameObject Area2;
    public GameObject Area2Buy;
    public GameObject Area2Lock;
    public GameObject Area3;
    public GameObject Area3Buy;
    public GameObject Area3Lock;
    public GameObject Area4;
    public GameObject Area4Buy;
    public GameObject Area4Lock;
    //public GameObject Area5;
    //public GameObject Area5Buy;
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

    public GameObject VendingMachine;
    public GameObject VendingMachineBuy;

    public GameObject Shop;

    public GameObject Machine1Upgrade;
    public GameObject Machine2Upgrade;
    public GameObject Machine3Upgrade;
    public GameObject Machine4Upgrade;
    public GameObject Machine5Upgrade;
    public GameObject Machine6Upgrade;
    public GameObject Machine7Upgrade;

    public GameObject buyers;
    public GameObject Level2Buy;
    public GameObject Level2Buy_Lock;

    public JSONSave jsonSave;
    public CanvasScript canvasScript;

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

        Machine1.SetActive(jsonSave.playerdata.Machine1Buy);
        Machine1Upgrade.SetActive(jsonSave.playerdata.Machine1Buy);
        if (jsonSave.playerdata.Machine1Buy)
        {
            Machine1Buy.SetActive(false);
            if (jsonSave.playerdata.Machine1Upgrade)
            {
                Machine1.GetComponentInChildren<MachineScript>().isDoneUpgrade = true;
                Destroy(Machine1Upgrade);
            }
            Car1.SetActive(jsonSave.playerdata.Car1Buy);
            if (jsonSave.playerdata.Car1Buy)
            {
                Car1Buy.SetActive(false);

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

                        Car2.SetActive(jsonSave.playerdata.Car2Buy);
                        if (jsonSave.playerdata.Car2Buy)
                        {
                            Car2Buy.SetActive(false);
                            Shop.SetActive(true);

                            Area2.SetActive(!jsonSave.playerdata.Area2Buy);
                            if (jsonSave.playerdata.Area2Buy)
                            {
                                Area2Buy.SetActive(false);
                                Destroy(Area2Lock);
                            }
                            else
                            {
                                Area2Buy.SetActive(true);
                                Destroy(Area2Lock);
                            }
                        }
                        else
                        {
                            Car2Buy.SetActive(true);
                        }
                    }
                    else
                    {
                        Machine2Buy.SetActive(true);
                    }
                }
                else
                {
                    Machine3Buy.SetActive(true);
                }
            }
            else
            {
                Car1Buy.SetActive(true);
            }
        }

        if (jsonSave.playerdata.Area2Buy)
        {
            Area2.SetActive(false);
            buyers.SetActive(true);
            Rack2.SetActive(jsonSave.playerdata.Rack2Buy);
            if (jsonSave.playerdata.Rack2Buy)
            {
                Rack2Buy.SetActive(false);

                Rack3.SetActive(jsonSave.playerdata.Rack3Buy);
                if (jsonSave.playerdata.Rack3Buy)
                {
                    Rack3Buy.SetActive(false);
                    if (jsonSave.playerdata.VendingMachingBuy)
                    {
                        VendingMachine.SetActive(true);
                        VendingMachineBuy.SetActive(false);
                        Destroy(Area3Lock);
                    }
                    else
                    {
                        VendingMachineBuy.SetActive(true);
                        Destroy(Area3Lock);
                    }
                }
                else
                {
                    Rack3Buy.SetActive(true);
                }
                Rack4.SetActive(jsonSave.playerdata.Rack4Buy);
                if (jsonSave.playerdata.Rack4Buy)
                {
                    Rack4Buy.SetActive(false);
                    if (jsonSave.playerdata.VendingMachingBuy)
                    {
                        VendingMachine.SetActive(true);
                        VendingMachineBuy.SetActive(false);
                        if (Area3Lock != null)
                        {
                            Destroy(Area3Lock);
                        }
                    }
                    else
                    {
                        VendingMachineBuy.SetActive(true);
                        if (Area3Lock != null)
                        {
                            Destroy(Area3Lock);
                        }
                    }
                }
                else
                {
                    Rack4Buy.SetActive(true);
                }
            }
            else
            {
                Rack2Buy.SetActive(true);
            }        
        }

        if (jsonSave.playerdata.VendingMachingBuy)
        {
            if (jsonSave.playerdata.Area3Buy)
            {
                Area3.SetActive(false);
                Area3Buy.SetActive(false);

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

                    Truck1.SetActive(jsonSave.playerdata.Truck1Buy);
                    if (jsonSave.playerdata.Truck1Buy)
                    {
                        Truck1Buy.SetActive(false);

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
                            Destroy(Area4Lock);
                        }
                        else
                        {
                            Machine6Buy.SetActive(true);
                            Destroy(Area4Lock);
                        }
                        Truck2.SetActive(jsonSave.playerdata.Truck2Buy);
                        if (jsonSave.playerdata.Truck2Buy)
                        {
                            Truck2Buy.SetActive(false);
                            if (Area4Lock != null)
                            {
                                Destroy(Area4Lock);
                            }
                        }
                        else
                        {
                            Truck2Buy.SetActive(true);
                            if (Area4Lock != null)
                            {
                                Destroy(Area4Lock);
                            }
                        }
                    }
                    else
                    {
                        Truck1Buy.SetActive(true);
                    }
                }
                else
                {
                    Machine4Buy.SetActive(true);
                }
            }
            else
            {
                Area3Buy.SetActive(true);
            }
        }

        if (jsonSave.playerdata.Truck2Buy || jsonSave.playerdata.Machine6Buy)
        {
            Area4.SetActive(!jsonSave.playerdata.Area4Buy);
            if (jsonSave.playerdata.Area4Buy)
            {
                Area4.SetActive(false);
                Area4Buy.SetActive(false);

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

                    Car6.SetActive(jsonSave.playerdata.Car6Buy);
                    if (jsonSave.playerdata.Car6Buy)
                    {
                        Car6Buy.SetActive(false);
                        Destroy(Level2Buy_Lock);

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
                        else
                        {
                            Machine5Buy.SetActive(true);
                        }
                        Car4.SetActive(jsonSave.playerdata.Car4Buy);
                        if (jsonSave.playerdata.Car4Buy)
                        {
                            Car4Buy.SetActive(false);
                        }
                        else
                        {
                            Car4Buy.SetActive(true);
                        }
                        Level2Buy.SetActive(true);
                    }
                    else
                    {
                        Car6Buy.SetActive(true);
                    }
                }
                else
                {
                    Machine7Buy.SetActive(true);
                }
            }
            else
            {
                Area4Buy.SetActive(true);
            }
        }



        Car3.SetActive(jsonSave.playerdata.Car3Buy);
        if (jsonSave.playerdata.Car3Buy)
        {
            Car3Buy.SetActive(false);
        }
        else
        {
            if (jsonSave.playerdata.Truck2Buy)
            {
                Car3Buy.SetActive(true);
            }
            if (jsonSave.playerdata.Machine6Buy)
            {
                Car3Buy.SetActive(true);
            }
        }
     
    }

    //Area 1
    public void BoughtMachine1()
    {
        Car1Buy.SetActive(true);
    }
    public void BoughtCar1()
    {
        Machine3Buy.SetActive(true);
    }
    public void BoughtMachine3()
    {
        Machine2Buy.SetActive(true);
    }
    public void BoughtMachine2()
    {
        Car2Buy.SetActive(true);
    }
    public void BoughtCar2()
    {
        Shop.SetActive(true);
        Area2Buy.SetActive(true);
        Destroy(Area2Lock);
    }

    //Area 2
    public void BoughtArea2()
    {       
        Rack2Buy.SetActive(true);
        buyers.SetActive(true);
    }
    public void BoughtRack2()
    {
        Rack3Buy.SetActive(true);
        Rack4Buy.SetActive(true);
    }
    public void BoughtRack3()
    {
        if (VendingMachineBuy == null)
        {
            return;
        }
        if (!VendingMachineBuy.activeInHierarchy && jsonSave.playerdata.VendingMachingBuy == false)
        {
            VendingMachineBuy.SetActive(true);
            Camera.main.GetComponent<CameraScript>().Rack3or4Lerp();
            canvasScript.UpdateSliderValue();
        }
    }
    public void BoughtRack4()
    {
        if (VendingMachineBuy == null)
        {
            return;
        }
        if (!VendingMachineBuy.activeInHierarchy && jsonSave.playerdata.VendingMachingBuy == false)
        {
            VendingMachineBuy.SetActive(true);
            Camera.main.GetComponent<CameraScript>().Rack3or4Lerp();
            canvasScript.UpdateSliderValue();
        }
    }
    public void BoughtVendingMachine()
    {
        Area3Buy.SetActive(true);
        Destroy(Area3Lock);
    }

    //Area 3
    public void BoughtArea3()
    {
        Machine4Buy.SetActive(true);
    }
    public void BoughtMachine4()
    {
        Truck1Buy.SetActive(true);
    }
    public void BoughtTruck1()
    {
        Machine6Buy.SetActive(true);
        Truck2Buy.SetActive(true);
    }
    public void BoughtMachine6()
    {
        if (Area4Buy == null)
        {
            return;
        }
        if (!Area4Buy.activeInHierarchy && jsonSave.playerdata.Area4Buy == false)
        {
            Area4Buy.SetActive(true);
            Destroy(Area4Lock);
            Camera.main.GetComponent<CameraScript>().Truck2orMachine6Lerp();
            canvasScript.UpdateSliderValue();
        }
        Car3Buy.SetActive(true);
    }
    public void BoughtTruck2()
    {
        if (Area4Buy == null)
        {
            return;
        }
        if (!Area4Buy.activeInHierarchy && jsonSave.playerdata.Area4Buy == false)
        {
            Area4Buy.SetActive(true);
            if (Area4Lock != null)
            {
                Destroy(Area4Lock);
            }
            Camera.main.GetComponent<CameraScript>().Truck2orMachine6Lerp();
            canvasScript.UpdateSliderValue();
        }
        Car3Buy.SetActive(true);
    }

    //Area 4
    public void BoughtArea4()
    {
        Machine7Buy.SetActive(true);
    }
    public void BoughtMachine7()
    {
        Car6Buy.SetActive(true);
    }
    public void BoughtCar6()
    {
        Level2Buy.SetActive(true);
        Car4Buy.SetActive(true);
        Machine5Buy.SetActive(true);
        Destroy(Level2Buy_Lock);
    }


    public void NextLevel()
    {
        Debug.Log(nextLevel);
        SceneManager.LoadScene(nextLevel);
    }
    
}
