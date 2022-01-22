using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class HireScript : MonoBehaviour
{
    public bool isLevel2 = false;
    private bool isHireUIShown = false;

    public GameObject player;
    public GameObject canvas;
    public JSONSave jSONSave;

    public GameObject HireUI;
    public GameObject HireUI_MoverAi_1_BuyButton;
    public GameObject HireUI_MoverAi_2_BuyButton;
    public GameObject HireUI_MoverAi_3_BuyButton;
    public GameObject HireUI_MoverAi_4_BuyButton;
    public GameObject HireUI_PlayerUpgradeSpeed_BuyButton;
    public GameObject HireUI_PlayerUpgradeCapacity_BuyButton;
    public GameObject HireUI_MoverUpgradeSpeed_BuyButton;

    public GameObject MoverAi_1;
    public GameObject MoverAi_2;
    public GameObject MoverAi_3;
    public GameObject MoverAi_4;
    public TrayScript machine1;
    public TrayScript machine2;
    public TrayScript machine3;
    public TrayScript machine4;
    public TrayScript machine5;
    public TrayScript machine6;

    public MoverAIScript[] moversScript;
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
        if (jSONSave.playerdata.MoverAi_3Buy)
        {
            MoverAi_3.SetActive(true);
            HireUI_MoverAi_3_BuyButton.SetActive(false);
        }
        if (jSONSave.playerdata.MoverAi_4Buy)
        {
            MoverAi_4.SetActive(true);
            HireUI_MoverAi_4_BuyButton.SetActive(false);
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
            machine5.isUpgradeCapacity = true;
            machine6.isUpgradeCapacity = true;
            HireUI_PlayerUpgradeCapacity_BuyButton.SetActive(false);
            canvas.GetComponent<CanvasScript>().isPlayerCapacityUpgrade = true;
        }
        if (jSONSave.playerdata.MoverAiUpgradeSpeed)
        {
            for (int i = 0; i < moversScript.Length; i++)
            {
                moversScript[i].isUpgradeSpeed = true;
            }
            HireUI_MoverUpgradeSpeed_BuyButton.SetActive(false);
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
            if (!isLevel2)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Ai1", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "L2_Ai1", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
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
            if (!isLevel2)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Ai2", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "L2_Ai2", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
        }
    }

    public void BuyMoverAi_3()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 450)
        {
            MoverAi_3.SetActive(true);
            HireUI.SetActive(false);
            HireUI_MoverAi_3_BuyButton.SetActive(false);
            jSONSave.playerdata.MoverAi_3Buy = true;
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 450;
            if (!isLevel2)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Ai3", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "L2_Ai3", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
        }
    }

    public void BuyMoverAi_4()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 1000)
        {
            MoverAi_4.SetActive(true);
            HireUI.SetActive(false);
            HireUI_MoverAi_4_BuyButton.SetActive(false);
            jSONSave.playerdata.MoverAi_4Buy = true;
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 1000;
            if (!isLevel2)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Ai4", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "L2_Ai4", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
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
            if (!isLevel2)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Speed", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "L2_Speed", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
        }
    }

    public void BuyPlayerUpgradeCapacity()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 150)
        {
            canvas.GetComponent<CanvasScript>().isPlayerCapacityUpgrade = true;
            jSONSave.playerdata.PlayerUpgradeCapacity = true;
            machine1.isUpgradeCapacity = true;
            machine2.isUpgradeCapacity = true;
            machine3.isUpgradeCapacity = true;
            machine5.isUpgradeCapacity = true;
            machine6.isUpgradeCapacity = true;
            HireUI.SetActive(false);
            HireUI_PlayerUpgradeCapacity_BuyButton.SetActive(false);          
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 150;
            if (!isLevel2)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Capacity", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "L2_Capacity", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
        }
    }

    public void BuyMoverUpgradeSpeed()
    {
        if (canvas.GetComponent<CanvasScript>().totalCoins >= 250)
        {
            for (int i = 0; i < moversScript.Length; i++)
            {
                moversScript[i].isUpgradeSpeed = true;
            }
            HireUI.SetActive(false);
            HireUI_MoverUpgradeSpeed_BuyButton.SetActive(false);
            jSONSave.playerdata.MoverAiUpgradeSpeed = true;
            jSONSave.SavaData();
            canvas.GetComponent<CanvasScript>().totalCoins -= 250;
            if (!isLevel2)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "MoverUpgradeSpeed", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "L2_MoverUpgradeSpeed", (int)canvas.GetComponent<CanvasScript>().totalCoins);
            }
        }
    }

    public void ShowUI(GameObject numUI)
    {
        numUI.SetActive(true);
    }
    public void CloseUI(GameObject numUI)
    {
        numUI.SetActive(false);
    }


}
