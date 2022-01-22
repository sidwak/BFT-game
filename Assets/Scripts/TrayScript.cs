using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrayScript : MonoBehaviour
{
    public int activeNoBoxes = 25;
    public int playerBoxesMax;
    public int MaxMachineBoxes;

    public bool isPlayerInArea = false;
    public bool isMoverInArea = false;
    public bool isConverterMachine = false;
    public bool is3box = false;
    public bool isAiTarget = false;
    public bool isUpgradeCapacity = false;

    public Vector2 offset;
    public Vector2 canvasScale;

    public GameObject player;
    public GameObject cart;
    private MoverBoxScript moverBoxScript;

    public List<GameObject> activeBoxesList = new List<GameObject>();
    public List<GameObject> inactiveBoxesList = new List<GameObject>();

    public GameObject MainTextObject;
    public GameObject MachineBoxTextObject;
    public GameObject MachineBoxFullTextObject;
    public PlayerBoxScript cartBoxScript;
    public TextMeshProUGUI MachineBoxText;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            activeBoxesList.Add(child.gameObject);
        }
        for (int i = 0; i < activeNoBoxes; i++)
        {
            inactiveBoxesList.Add(activeBoxesList[activeBoxesList.Count - 1]);
            inactiveBoxesList[inactiveBoxesList.Count - 1].SetActive(false);
            activeBoxesList.RemoveAt(activeBoxesList.Count - 1);
        }
        /*for (int i = 0; i < activeNoBoxes; i++)
        {
            BoxesList[i].SetActive(true);
        }*/
        inactiveBoxesList.Reverse();

        MainTextObject.SetActive(true);
        /*Vector3 numPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 TextnumPos = MainTextObject.GetComponent<RectTransform>().anchoredPosition;
        offset = TextnumPos - new Vector2(numPos.x, numPos.y); */         //debug log to get value and set for all machines

        canvasScale = new Vector2(1080f/Screen.width, 1920f/Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 numPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z));
        Vector2 Posnum = new Vector2(numPos.x, numPos.y);
        Posnum.x *= canvasScale.x;
        Posnum.y *= canvasScale.y;
        MainTextObject.GetComponent<RectTransform>().anchoredPosition = Posnum;
        if (activeBoxesList.Count < MaxMachineBoxes)                        //set for all machines objects or gets error
        {
            MachineBoxTextObject.SetActive(true);
            MachineBoxFullTextObject.SetActive(false);
            MachineBoxText.text = activeBoxesList.Count.ToString();
        }
        else
        {
            MachineBoxFullTextObject.SetActive(true);
            MachineBoxTextObject.SetActive(false);
        }

        if (is3box)
        {
            if (activeBoxesList.Count >= 1)  //previous value 3
            {
                if (isMoverInArea)
                {
                    StartCoroutine(MoverStaying());
                    is3box = false;
                }
                if (isPlayerInArea)
                {
                    if (!isConverterMachine)
                    {
                        StartCoroutine(PlayerStaying());
                    }
                    else
                    {
                        StartCoroutine(CartPlayerStaying());
                    }
                    is3box = false;
                }              
            }
        }

        if (isUpgradeCapacity)
        {
            playerBoxesMax = 60;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "MoverAI" && isConverterMachine == false)
        {
            moverBoxScript = other.GetComponentInChildren<MoverBoxScript>();
            isMoverInArea = true;
            StartCoroutine(MoverStaying());
            return;
        }
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        //Debug.Log(other.gameObject.name);
        //layerEntered();
        isPlayerInArea = true;
        if (!isConverterMachine)
        {
            StartCoroutine(PlayerStaying());
        }
        else
        {
            if (!cart.activeInHierarchy)
            {
                cart.SetActive(true);
                cartBoxScript.pauseCartDisable = true;
            }
            StartCoroutine(CartPlayerStaying());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MoverAI")
        {
            isMoverInArea = false;
        }
        else
        {
            isPlayerInArea = false;
        }
        if (isConverterMachine) { 
            if (cart.activeInHierarchy)
            {
                cartBoxScript.pauseCartDisable = false;
            }
        }
    }

    IEnumerator PlayerStaying()
    {
        yield return new WaitForSeconds(0.075f);
        if (activeBoxesList.Count == 0)
        {
            is3box = true;
        }
        if (activeBoxesList.Count == 0 || player.GetComponent<PlayerBoxScript>().boxNumber == playerBoxesMax)
        {
            yield break;
        }
        GameObject refBox = null;
        int pos = 0;
        for (int i = activeBoxesList.Count-1; i >= 0; i--)
        {
            if (activeBoxesList[i].activeInHierarchy)
            {
                refBox = activeBoxesList[i];
                pos = i;
                break;
            }
        }
        if (refBox == null)
        {
            StartCoroutine(PlayerStaying());
            yield break;
        }
        //GameObject num = Instantiate(activeBoxesList[0], activeBoxesList[activeBoxesList.Count-1].transform.position, activeBoxesList[0].transform.rotation);
        GameObject num = Instantiate(activeBoxesList[0], refBox.transform.position, activeBoxesList[0].transform.rotation);
        num.AddComponent<BoxScript>();
        int numbox = player.GetComponent<PlayerBoxScript>().boxNumber;
        num.GetComponent<BoxScript>().targetobject = player.GetComponent<PlayerBoxScript>().PlayerBoxesList[numbox];
        num.GetComponent<BoxScript>().isTargetset = true;
        player.GetComponent<PlayerBoxScript>().boxNumber++;
        //inactiveBoxesList.Insert(0, activeBoxesList[activeBoxesList.Count - 1]);
        inactiveBoxesList.Insert(0, refBox);
        inactiveBoxesList[0].SetActive(false);
        //activeBoxesList.RemoveAt(activeBoxesList.Count - 1);
        activeBoxesList.RemoveAt(pos);
        activeNoBoxes--;
        if (isPlayerInArea)
        {
            StartCoroutine(PlayerStaying());
        }      
    }

    IEnumerator MoverStaying()
    {
        yield return new WaitForSeconds(0.075f);
        if (activeBoxesList.Count == 0)
        {
            is3box = true;
        }
        if (activeBoxesList.Count == 0 || moverBoxScript.boxNumber == 10)
        {
            yield break;
        }
        GameObject refBox = null;
        int pos = 0;
        for (int i = activeBoxesList.Count - 1; i >= 0; i--)
        {
            if (activeBoxesList[i].activeInHierarchy)
            {
                refBox = activeBoxesList[i];
                pos = i;
                break;
            }
        }
        if (refBox == null)
        {
            StartCoroutine(MoverStaying());
            yield break;
        }
        //GameObject num = Instantiate(activeBoxesList[0], activeBoxesList[activeBoxesList.Count-1].transform.position, activeBoxesList[0].transform.rotation);
        GameObject num = Instantiate(activeBoxesList[0], refBox.transform.position, activeBoxesList[0].transform.rotation);
        num.AddComponent<BoxScript>();
        int numbox = moverBoxScript.boxNumber;
        num.GetComponent<BoxScript>().targetobject = moverBoxScript.PlayerBoxesList[numbox];
        num.GetComponent<BoxScript>().isTargetset = true;
        moverBoxScript.boxNumber++;
        //inactiveBoxesList.Insert(0, activeBoxesList[activeBoxesList.Count - 1]);
        inactiveBoxesList.Insert(0, refBox);
        inactiveBoxesList[0].SetActive(false);
        //activeBoxesList.RemoveAt(activeBoxesList.Count - 1);
        activeBoxesList.RemoveAt(pos);
        activeNoBoxes--;
        if (isMoverInArea)
        {
            StartCoroutine(MoverStaying());
        }
    }

    IEnumerator CartPlayerStaying()
    {
        /*if (!cart.activeInHierarchy)
        {
            cart.SetActive(true);
        }*/
        yield return new WaitForSeconds(0.075f);
        if (activeBoxesList.Count == 0)
        {
            is3box = true;
        }
        if (activeBoxesList.Count == 0 || cartBoxScript.boxNumber == playerBoxesMax)
        {
            yield break;
        }
        GameObject refBox = null;
        int pos = 0;
        for (int i = activeBoxesList.Count - 1; i >= 0; i--)
        {
            if (activeBoxesList[i].activeInHierarchy)
            {
                refBox = activeBoxesList[i];
                pos = i;
                break;
            }
        }
        if (refBox == null)
        {
            StartCoroutine(CartPlayerStaying());
            yield break;
        }
        //GameObject num = Instantiate(activeBoxesList[0], activeBoxesList[activeBoxesList.Count-1].transform.position, activeBoxesList[0].transform.rotation);
        GameObject num = Instantiate(activeBoxesList[0], refBox.transform.position, activeBoxesList[0].transform.rotation);
        num.AddComponent<BoxScript>();
        int numbox = cartBoxScript.boxNumber;
        num.GetComponent<BoxScript>().targetobject = cartBoxScript.PlayerBoxesList[numbox];
        num.GetComponent<BoxScript>().isTargetset = true;
        cartBoxScript.boxNumber++;
        //inactiveBoxesList.Insert(0, activeBoxesList[activeBoxesList.Count - 1]);
        inactiveBoxesList.Insert(0, refBox);
        inactiveBoxesList[0].SetActive(false);
        //activeBoxesList.RemoveAt(activeBoxesList.Count - 1);
        activeBoxesList.RemoveAt(pos);
        activeNoBoxes--;
        if (isPlayerInArea)
        {
            StartCoroutine(CartPlayerStaying());
        }
    }
}
