using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameAnalyticsSDK;

public class BuyMachineScript : MonoBehaviour
{

    public float buyAmount;
    public float buyIronAmount;
    public float buyMultiplier;

    public bool isRevert = false;
    public bool isMachine = false;
    public bool isMachineUpgrade = false;
    public bool isFake = false;
    public bool useBuyMultiplier = false;
    public bool isNextLevel = false;
    public bool isLevelFake = false;
    public bool isEventCalled = false;
    private bool isUIshown = false;

    public GameObject NewMachine;
    public GameObject MachineUpgrade;
    public GameObject player;
    public GameObject buySystem;
    public GameObject canvas;
    public GameObject buyObjectPos;

    public TextMeshPro buyText;
    public TextMeshPro ironBuyText;

    public JSONSave jsonSave;

    public MachineScript machineScript;
    public CanvasScript canvasScript;
    public CameraScript cameraScript;
    public GameObject upgradeMachine_UI;
    private CharacterScript characterScript;

    public PlayerBoxScript mainBoxScript;
    public PlayerBoxScript ironBoxScript;

    public string str;
    public string Level_str;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = player.GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buyAmount <= 0 && isFake == false && buyIronAmount <= 0)
        {
            if (!isRevert)
            {
                if(isMachine)
                {
                    NewMachine.GetComponentInChildren<MachineScript>().isBuy = true;
                    MachineUpgrade.SetActive(true);
                }
                NewMachine.SetActive(true);              
                //jsonSave.playerdata.Machine2Buy = true;
                //jsonSave.SavaData();                         
                SaveState();
            }
            else
            {
                if (!isNextLevel)
                {
                    NewMachine.SetActive(false);
                }
                /*if (str == "Area2Buy")
                {
                    buySystem.GetComponent<BuySystem>().BoughtArea2();
                }
                if (str == "Area3Buy")
                {
                    buySystem.GetComponent<BuySystem>().BoughtArea3();
                }
                if (str == "Area4Buy")
                {
                    buySystem.GetComponent<BuySystem>().BoughtArea4();
                }*/
                if (str == "Level2Buy")
                {
                    if (!isLevelFake)
                    {
                        buySystem.GetComponent<BuySystem>().NextLevel();
                    }
                }
                SaveState();
            }           
            Destroy(gameObject, 0.5f);
        }
        if (isFake)
        {
            if (buyAmount <= 0)
            {
                if (!jsonSave.playerdata.isFakeSend)
                {
                    /*GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Zone 4", (int)canvasScript.totalCoins);
                    jsonSave.playerdata.isFakeSend = true;*/
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        if (!isMachineUpgrade)
        {
            if (buyAmount > 0f && characterScript.isMoving == false)
            {
                if (canvas.GetComponent<CanvasScript>().totalCoins > 0)
                {
                    if (useBuyMultiplier)
                    {
                        if (canvas.GetComponent<CanvasScript>().totalCoins <= buyMultiplier)
                        {
                            useBuyMultiplier = false;
                        }
                    }
                    if (!useBuyMultiplier)
                    {
                        buyAmount--;
                        canvas.GetComponent<CanvasScript>().totalCoins--;
                    }
                    else
                    {
                        buyAmount -= buyMultiplier;
                        canvas.GetComponent<CanvasScript>().totalCoins -= buyMultiplier;
                    }
                    buyText.text = buyAmount.ToString();
                    
                }
            }
            if (buyIronAmount > 0f && characterScript.isMoving == false && ironBoxScript.ironCount > 0)
            {
                if (canvas.GetComponent<CanvasScript>().totalIrons > 0)
                {
                    buyIronAmount--;
                    canvas.GetComponent<CanvasScript>().totalIrons--;
                    ironBuyText.text = buyIronAmount.ToString();
                    for (int i = ironBoxScript.PlayerBoxesList.Count-1; i >= 0; i--)
                    {
                        if (ironBoxScript.PlayerBoxesList[i].activeInHierarchy)
                        {
                            ironBoxScript.PlayerBoxesList[i].SetActive(false);
                            GameObject num = Instantiate(ironBoxScript.PlayerBoxesList[0], ironBoxScript.PlayerBoxesList[i].transform.position, ironBoxScript.PlayerBoxesList[i].transform.rotation);
                            num.SetActive(true);
                            num.AddComponent<BoxScript>();
                            num.GetComponent<BoxScript>().targetobject = buyObjectPos;
                            num.GetComponent<BoxScript>().isTargetset = true;
                            mainBoxScript.boxNumber--;
                            ironBoxScript.ironCount--;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            if (machineScript.isDoneUpgrade)
            {
                Destroy(gameObject);
                SaveState();
            }
            characterScript = player.GetComponent<CharacterScript>();
            if (characterScript.isMoving == false)
            {
                if (!isUIshown)
                {
                    upgradeMachine_UI.SetActive(true);
                    canvasScript.targetMachineScript = machineScript;
                    isUIshown = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (isMachineUpgrade)
        {
            upgradeMachine_UI.SetActive(true);
            canvasScript.targetMachineScript = machineScript;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        isUIshown = false;
    }

    public void SaveState()
    {
        if (isEventCalled == false)
        {
            isEventCalled = true;
        }
        else
        {
            return;
        }
        switch (str)
        {
            case "Machine1Buy":
                jsonSave.playerdata.Machine1Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtMachine1();
                cameraScript.Machine1Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Machine1", (int)canvasScript.totalCoins);
                break;
            case "Machine2Buy":
                jsonSave.playerdata.Machine2Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtMachine2();
                cameraScript.Machine2Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Machine2", (int)canvasScript.totalCoins);
                break;
            case "Machine3Buy":
                jsonSave.playerdata.Machine3Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtMachine3();
                cameraScript.Machine3Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Machine3", (int)canvasScript.totalCoins);
                break;
            case "Machine4Buy":
                jsonSave.playerdata.Machine4Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtMachine4();
                cameraScript.Machine4Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Machine4", (int)canvasScript.totalCoins);
                break;
            case "Machine5Buy":
                jsonSave.playerdata.Machine5Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Machine5", (int)canvasScript.totalCoins);
                break;
            case "Machine6Buy":
                jsonSave.playerdata.Machine6Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtMachine6();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Machine6", (int)canvasScript.totalCoins); 
                break;
            case "Machine7Buy":
                jsonSave.playerdata.Machine7Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtMachine7();
                cameraScript.Machine7Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Machine7", (int)canvasScript.totalCoins);
                break;
            case "Car1Buy":
                jsonSave.playerdata.Car1Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtCar1();
                cameraScript.Car1Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Car1", (int)canvasScript.totalCoins);
                break;
            case "Car2Buy":
                jsonSave.playerdata.Car2Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtCar2();
                cameraScript.Car2Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Car2", (int)canvasScript.totalCoins);
                break;
            case "Car3Buy":
                jsonSave.playerdata.Car3Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Car3", (int)canvasScript.totalCoins);
                break;
            case "Car4Buy":
                jsonSave.playerdata.Car4Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Car4", (int)canvasScript.totalCoins);
                break;
            case "Car6Buy":
                jsonSave.playerdata.Car6Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtCar6();
                cameraScript.Car6Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Car6", (int)canvasScript.totalCoins);
                break;
            case "Truck1Buy":
                jsonSave.playerdata.Truck1Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtTruck1();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Truck1", (int)canvasScript.totalCoins);
                break;
            case "Truck2Buy":
                jsonSave.playerdata.Truck2Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtTruck2();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Truck2", (int)canvasScript.totalCoins);
                break;
            case "Rack_2":
                jsonSave.playerdata.Rack2Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtRack2();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Rack2", (int)canvasScript.totalCoins);
                break;
            case "Rack_3":
                jsonSave.playerdata.Rack3Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtRack3();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Rack3", (int)canvasScript.totalCoins);
                break;
            case "Rack_4":
                jsonSave.playerdata.Rack4Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtRack4();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Rack4", (int)canvasScript.totalCoins);
                break;
            case "Area2Buy":
                jsonSave.playerdata.Area2Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtArea2();
                Camera.main.GetComponent<CameraScript>().Zone2Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Zone2", (int)canvasScript.totalCoins);
                break;
            case "Area3Buy":
                jsonSave.playerdata.Area3Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtArea3();
                Camera.main.GetComponent<CameraScript>().Zone3Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Zone3", (int)canvasScript.totalCoins);
                break;
            case "Area4Buy":
                jsonSave.playerdata.Area4Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtArea4();
                Camera.main.GetComponent<CameraScript>().Zone4Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str + "Zone4", (int)canvasScript.totalCoins);
                break;
            case "Machine1Upgrade":
                jsonSave.playerdata.Machine1Upgrade = true;
                break;
            case "Machine2Upgrade":
                jsonSave.playerdata.Machine2Upgrade = true;
                break;
            case "Machine3Upgrade":
                jsonSave.playerdata.Machine3Upgrade = true;
                break;
            case "Machine4Upgrade":
                jsonSave.playerdata.Machine4Upgrade = true;
                break;
            case "Machine5Upgrade":
                jsonSave.playerdata.Machine5Upgrade = true;
                break;
            case "Machine6Upgrade":
                jsonSave.playerdata.Machine6Upgrade = true;
                break;
            case "Level2Buy":
                jsonSave.playerdata.curLevel = 2;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Level_str, (int)canvasScript.totalCoins);
                break;
            case "VendingMachine1":
                jsonSave.playerdata.VendingMachingBuy= true;
                buySystem.GetComponent<BuySystem>().BoughtVendingMachine();
                cameraScript.VendingMachineLerp();
                break;
        }
        jsonSave.SavaData();
    }
}
