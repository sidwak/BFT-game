using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public float totalBoxes;
    public float totalCoins;
    public float totalIrons;

    public bool isPlayerCapacityUpgrade = false;

    private Vector2 numPos;
    private Vector2 canvasScale;
    private Vector2 holdPos;

    public TextMeshProUGUI totalBoxesText;
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI totalIronText;

    public JSONSave jsonSave;

    public GameObject headoverUI;
    public GameObject machineUpgrade_UI;
    public GameObject ironTextObject;
    public Joystick JYstick;

    public MachineScript targetMachineScript;

    string maxBoxCountnum = "30";


    // Start is called before the first frame update
    void Start()
    {
        TinySauce.OnGameStarted();
        StartCoroutine(WaitForDelay());
        //headoverUI.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        Vector2 numScaler = gameObject.GetComponent<RectTransform>().sizeDelta;
        //canvasScale = new Vector2(1080f / numScaler.x, 1920f / numScaler.y);
        //Debug.Log(canvasScale);
        //canvasScale = new Vector2(1080f / Screen.width, 1920f / Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerCapacityUpgrade)
        {
            maxBoxCountnum = "60";
        }
        totalBoxesText.text = totalBoxes.ToString();
        if (totalIrons > 0)
        {
            ironTextObject.SetActive(true);
            totalIronText.text = totalIrons.ToString();
        }
        else
        {
            ironTextObject.SetActive(false);
        }

        if (totalCoins >= 10000)
        {
            totalCoinsText.text = (totalCoins / 1000).ToString("0.#") + "K";
        }
        else if(totalCoins >= 1000)
        {
            totalCoinsText.text = (totalCoins / 1000).ToString("0.##") + "k";
        }
        else if (totalCoins >= 100)
        {
            totalCoinsText.text = totalCoins.ToString();
        }
        else if (totalCoins >= 10)
        {
            totalCoinsText.text = totalCoins.ToString();
        }
        else
        {
            totalCoinsText.text = totalCoins.ToString();
        }
        //totalCoinsText.text = totalCoins.ToString();

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(0.2f);
        totalCoins = jsonSave.playerdata.totalCoins;
        StartCoroutine(EverySave());
    }

    IEnumerator EverySave()
    {
        yield return new WaitForSeconds(30f);
        jsonSave.playerdata.totalCoins = (int)totalCoins;
        jsonSave.SavaData();       
        StartCoroutine(EverySave());
    }

    private void OnApplicationQuit()
    {
        TinySauce.OnGameFinished(totalCoins);
        jsonSave.playerdata.totalCoins = (int)totalCoins;
        jsonSave.SavaData();
    }

    public void MachineUpgrad()
    {
        if (totalCoins >= 60)
        {
            targetMachineScript.isDoneUpgrade = true;
            CloseMachineUpgrade_U();
            totalCoins -= 60;
        }
    }

    public void CloseMachineUpgrade_U()
    {
        machineUpgrade_UI.SetActive(false);
    }
}
