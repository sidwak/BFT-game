﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameAnalyticsSDK;

public class BuyMachineScript : MonoBehaviour
{

    public float buyAmount;
    public float buyMultiplier;

    public bool isRevert = false;
    public bool isMachine = false;
    public bool isMachineUpgrade = false;
    public bool isFake = false;
    public bool useBuyMultiplier = false;
    private bool isUIshown = false;

    public GameObject NewMachine;
    public GameObject MachineUpgrade;
    public GameObject player;
    public GameObject buySystem;
    public GameObject canvas;

    public TextMeshPro buyText;

    public JSONSave jsonSave;

    public MachineScript machineScript;
    public CanvasScript canvasScript;
    public GameObject upgradeMachine_UI;
    private CharacterScript characterScript;

    public string str;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = player.GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buyAmount <= 0 && isFake == false)
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
                NewMachine.SetActive(false);
                if (str == "Area2Buy")
                {
                    buySystem.GetComponent<BuySystem>().WallBuy1();
                }
                if (str == "Area3Buy")
                {
                    buySystem.GetComponent<BuySystem>().WallBuy2();
                }
                SaveState();
            }           
            Destroy(gameObject);
        }
        if (isFake)
        {
            if (buyAmount <= 0)
            {
                if (!jsonSave.playerdata.isFakeSend)
                {
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Zone 4", (int)canvasScript.totalCoins);
                    jsonSave.playerdata.isFakeSend = true;
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
        switch (str)
        {
            case "Machine2Buy":
                jsonSave.playerdata.Machine2Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtMachine2();
                Camera.main.GetComponent<CameraScript>().Zone1Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Machine2", (int)canvasScript.totalCoins);
                break;
            case "Machine3Buy":
                jsonSave.playerdata.Machine3Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Machine3", (int)canvasScript.totalCoins);
                break;
            case "Machine4Buy":
                jsonSave.playerdata.Machine4Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Machine4", (int)canvasScript.totalCoins);
                break;
            case "Machine5Buy":
                jsonSave.playerdata.Machine5Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Machine5", (int)canvasScript.totalCoins);
                break;
            case "Machine6Buy":
                jsonSave.playerdata.Machine6Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Machine6", (int)canvasScript.totalCoins);
                break;
            case "Car2Buy":
                jsonSave.playerdata.Car2Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Car2", (int)canvasScript.totalCoins);
                break;
            case "Car3Buy":
                jsonSave.playerdata.Car3Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtCar3();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Car3", (int)canvasScript.totalCoins);
                break;
            case "Car4Buy":
                jsonSave.playerdata.Car4Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Car4", (int)canvasScript.totalCoins);
                break;
            case "Car5Buy":
                jsonSave.playerdata.Car5Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Car5", (int)canvasScript.totalCoins);
                break;
            case "Truck1Buy":
                jsonSave.playerdata.Truck1Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Truck1", (int)canvasScript.totalCoins);
                break;
            case "Truck2Buy":
                jsonSave.playerdata.Truck2Buy = true;
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Truck2", (int)canvasScript.totalCoins);
                break;
            case "Area2Buy":
                jsonSave.playerdata.Area2Buy = true;
                buySystem.GetComponent<BuySystem>().BoughtArea2();
                Camera.main.GetComponent<CameraScript>().Zone2Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Zone2", (int)canvasScript.totalCoins);
                break;
            case "Area3Buy":
                jsonSave.playerdata.Area3Buy = true;
                Camera.main.GetComponent<CameraScript>().Zone3Lerp();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Zone3", (int)canvasScript.totalCoins);
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
        }
        jsonSave.SavaData();
    }
}
