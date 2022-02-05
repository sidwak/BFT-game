using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertedTrayScript : MonoBehaviour
{
    public int boxCountnum = 25;
    public int maxBoxCount;

    public bool playerInArea = false;


    public Vector2 offset;
    public Vector2 canvasScale;

    //public GameObject canvas;


    public PlayerBoxScript playerBoxScript;

    //public GameObject CarTextObject;
    //public TextMeshProUGUI BoxCountText;

    public List<GameObject> trayBoxesList = new List<GameObject>();


    void Start()
    {
        foreach (Transform child in transform)
        {
            trayBoxesList.Add(child.gameObject);
        }
        for (int i = 0; i < trayBoxesList.Count; i++)
        {
            trayBoxesList[i].SetActive(false);
        }
        for (int i = 0; i < boxCountnum; i++)
        {
            trayBoxesList[i].SetActive(true);
        }

        //CarTextObject.SetActive(true);
        //canvasScale = new Vector2(1080f / Screen.width, 1920f / Screen.height);
    }


    void Update()
    {

        Vector3 numPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z));
        Vector2 Posnum;
        Posnum.x = numPos.x;
        Posnum.y = numPos.y;
        Posnum.x *= canvasScale.x;
        Posnum.y *= canvasScale.y;

        //CarTextObject.GetComponent<RectTransform>().anchoredPosition = Posnum;

        //BoxCountText.text = boxCountnum.ToString() + "/" + maxBoxCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        playerInArea = true;
        StartCoroutine(CartPlayerStay());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        playerInArea = false;
    }

    IEnumerator CartPlayerStay()
    {
        yield return new WaitForSeconds(0.075f);
        if (playerBoxScript.breadCount == 0 || boxCountnum == maxBoxCount)
        {
            yield break;
        }
        int curBox = playerBoxScript.boxNumber - 1;
        for (int i = 0; i < playerBoxScript.PlayerBoxesList.Count; i++)
        {
            if (playerBoxScript.PlayerBoxesList[i].activeInHierarchy)
            {
                curBox = i;
            }
        }
        playerBoxScript.PlayerBoxesList[curBox].SetActive(false);
        GameObject refBox = playerBoxScript.PlayerBoxesList[curBox];
        playerBoxScript.breadCount--;
        playerBoxScript.boxNumber--;
        GameObject numBox = Instantiate(playerBoxScript.PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
        numBox.SetActive(true);
        //GameObject numCoin = Instantiate(CoinPrefab, numBox.transform.position, CoinPrefab.transform.rotation);
        //numCoin.GetComponent<CoinAnimationScript>().endTarget = coinEndPostion;
        numBox.AddComponent<BoxScript>();
        numBox.GetComponent<BoxScript>().targetobject = trayBoxesList[boxCountnum];
        numBox.GetComponent<BoxScript>().isTargetset = true;
        boxCountnum++;
        if (playerInArea)
        {
            StartCoroutine(CartPlayerStay());
        }
    }
}
