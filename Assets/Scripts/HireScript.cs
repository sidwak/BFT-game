using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class HireScript : MonoBehaviour
{
    private bool isHireUIShown = false;

    public GameObject player;
    public GameObject canvas;
    public JSONSave jSONSave;

    public GameObject HireUI;
    public GameObject HireUI_MoverAi_1_BuyButton;
    public GameObject HireUI_MoverAi_2_BuyButton;
    public GameObject HireUI_PlayerUpgradeSpeed_BuyButton;
    public GameObject HireUI_PlayerUpgradeCapacity_BuyButton;

    public GameObject MoverAi_1;
    public GameObject MoverAi_2;
    public TrayScript machine1;
    public TrayScript machine2;
    public TrayScript machine3;
    public TrayScript machine4;
    public TrayScript machine5;
    public TrayScript machine6;

    private CharacterScript characterScript;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = player.GetComponent<CharacterScript>();
        StartCoroutine(WaitForLoad());
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.6f);
        if (jSONSave.playerdata.MoverAi_1Buy)
        {
            MoverAi_1.SetActive(true);
            HireUI_MoverAi_1_BuyButton.SetActive(false);
        }
        if (jSONSave.playerdata.MoverAi_2Buy)
        {
            MoverAi_2.SetActive(true);
            HireUI_MoverAi_2_BuyButton.SetActive(false);
        }
        if (jSONSave.playerdata.PlayerUpgradeSpeed)
        {
            characterScript.isSpeedUpgrade = true;
            HireUI_PlayerUpgradeSpeed_BuyButton.SetActive(false);
        }
        if (jSONSave.playerdata.PlayerUpgradeCapacity)
        {
            machine1.isUpgradeCapacity = true;
            machine2.isUpgradeCapacity = true;
            machine3.isUpgradeCapacity = true;
            machine4.isUpgradeCapacity = true;
            machine5.isUpgradeCapacity = true;
            machine6.isUpgradeCapacity = true;
            HireUI_PlayerUpgradeCapacity_BuyButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        if (characterScript.isMoving == false)
        {
            if (!isHireUIShown)
            {
                isHireUIShown = true;
                HireUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        isHireUIShown = false;
    }
    public void BuyMoverAi_1()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 100)
        {
            MoverAi_1.SetActive(true);
            HireUI.SetActive(false);
            HireUI_MoverAi_1_BuyButton.SetActive(false);
            jSONSave.playerdata.MoverAi_1Buy = true;
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 100;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Ai1", (int)canvas.GetComponent<CanvasScript>().totalCoins);
        }
    }

    public void BuyMoverAi_2()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 250)
        {
            MoverAi_2.SetActive(true);
            HireUI.SetActive(false);
            HireUI_MoverAi_2_BuyButton.SetActive(false);
            jSONSave.playerdata.MoverAi_2Buy = true;
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 250;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Ai2", (int)canvas.GetComponent<CanvasScript>().totalCoins);
        }
    }

    public void BuyPlayerUpgradeSpeed()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 150)
        {
            characterScript.isSpeedUpgrade = true;
            HireUI.SetActive(false);
            HireUI_PlayerUpgradeSpeed_BuyButton.SetActive(false);
            jSONSave.playerdata.PlayerUpgradeSpeed = true;
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 150;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Speed", (int)canvas.GetComponent<CanvasScript>().totalCoins);
        }
    }

    public void BuyPlayerUpgradeCapacity()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 150)
        {
            jSONSave.playerdata.PlayerUpgradeCapacity = true;
            machine1.isUpgradeCapacity = true;
            machine2.isUpgradeCapacity = true;
            machine3.isUpgradeCapacity = true;
            machine4.isUpgradeCapacity = true;
            machine5.isUpgradeCapacity = true;
            machine6.isUpgradeCapacity = true;
            HireUI.SetActive(false);
            HireUI_PlayerUpgradeCapacity_BuyButton.SetActive(false);          
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 150;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Capacity", (int)canvas.GetComponent<CanvasScript>().totalCoins);
        }
    }

    public void CloseHireUI()
    {
        HireUI.SetActive(false);
    }
}
