using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBoxScript : MonoBehaviour
{
    public int boxNumber = 0;
    public int maxBoxCount = 30;
    public int breadCount = 0;
    public int bread2Count = 0;
    public int ironCount = 0;
    public int lastBoxNumber = 0;

    public bool isConverted = false;
    public bool pauseCartDisable = false;
    public bool isMain = false;
    public bool is2Bread = false;
    public bool isIron = false;

    public GameObject canvas;
    public GameObject maxTextObject;
    public GameObject cart;
    public GameObject cartonBoxTextObject;

    public TextMeshProUGUI cartonBoxText;

    public List<GameObject> PlayerBoxesList = new List<GameObject>();
    public PlayerBoxScript mainBoxScript;
    public PlayerBoxScript bread2BoxScript;
    public PlayerBoxScript ironBoxScript;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            PlayerBoxesList.Add(child.gameObject);
        }
        for (int i = 0; i < PlayerBoxesList.Count; i++)
        {
            PlayerBoxesList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMain)
        {
            canvas.GetComponent<CanvasScript>().totalBoxes = breadCount;
            if (boxNumber == maxBoxCount)
            {
                maxTextObject.SetActive(true);
                maxTextObject.transform.position = new Vector3(transform.position.x, maxTextObject.transform.position.y, transform.position.z);
            }
            else
            {
                maxTextObject.SetActive(false);
            }
        }
        else if (isIron)
        {
            canvas.GetComponent<CanvasScript>().totalIrons = ironCount;
        }
        

        if (isConverted)
        {
            if (boxNumber == 0 && pauseCartDisable == false)
            {
                cart.SetActive(false);
            }
            if (boxNumber > 0)
            {
                cartonBoxTextObject.SetActive(true);
                cartonBoxText.text = boxNumber.ToString() + "/" + "6";
            }
            else
            {
                cartonBoxTextObject.SetActive(false);
            }
        }

        if (isMain)
        {
            if (boxNumber > 0)
            {
                if (boxNumber < lastBoxNumber)
                {
                    BreadPosUpdate();
                    lastBoxNumber = boxNumber;
                }
                else
                {
                    lastBoxNumber = boxNumber;
                }
            }
        }
    }

    public void BreadPosUpdate()
    {
        int NumBox = boxNumber;
        int Diff = NumBox - breadCount;
        for (int i = 0; i < PlayerBoxesList.Count; i++)
        {
            PlayerBoxesList[i].SetActive(false);
        }
        for (int i = 0; i < breadCount; i++)
        {
            PlayerBoxesList[i].SetActive(true);
        }
        ironBoxScript.IronPosUpdate();
    }

    public void IronPosUpdate()
    {
        int NumBox = mainBoxScript.boxNumber;
        int Diff = NumBox - ironCount;
        for (int i = 0; i < PlayerBoxesList.Count; i++)
        {
            PlayerBoxesList[i].SetActive(false);
        }
        for (int i = mainBoxScript.breadCount; i < mainBoxScript.breadCount+ironCount; i++)
        {
            PlayerBoxesList[i].SetActive(true);
        }
    }
}
