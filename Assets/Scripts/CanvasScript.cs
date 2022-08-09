using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Toggle vibrationToggle;
    public Toggle soundToggle;
    public Slider slider;

    public AudioSource audioSource;

    public JSONSave jsonSave;

    public GameObject headoverUI;
    public GameObject machineUpgrade_UI;
    public GameObject ironTextObject;
    public Joystick JYstick;

    public MachineScript targetMachineScript;

    public string maxBoxCountnum = "30";


    // Start is called before the first frame update
    void Start()
    {
                                                                                                            //TinySauce.OnGameStarted();
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
        //                          SET TOGGLES
        vibrationToggle.isOn = jsonSave.playerdata.vibrationToggle;
        MobileVIbration.isVibrationActive = vibrationToggle.isOn;
        soundToggle.isOn = jsonSave.playerdata.soundToggle;
        audioSource.mute = !jsonSave.playerdata.soundToggle;
        StartCoroutine(EverySave());
        //slider.value = jsonSave.playerdata.sliderVal / 17f;
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
                                                                                                //TinySauce.OnGameFinished(totalCoins);
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

    public void ToggleValueChanged(GameObject toggle)
    {
        if (toggle.name == "VibrationToggle") 
        {
            MobileVIbration.isVibrationActive = toggle.GetComponentInChildren<Toggle>().isOn;
            jsonSave.playerdata.vibrationToggle = toggle.GetComponentInChildren<Toggle>().isOn;
            jsonSave.SavaData();
        }
        else if (toggle.name == "SoundToggle")
        {
            audioSource.mute = !toggle.GetComponentInChildren<Toggle>().isOn;
            jsonSave.playerdata.soundToggle = toggle.GetComponentInChildren<Toggle>().isOn;
            jsonSave.SavaData();
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

    public void UpdateSliderValue()
    {
        jsonSave.playerdata.sliderVal += 1f;
        //slider.value = jsonSave.playerdata.sliderVal / 17f;
        jsonSave.SavaData();
    }
}
