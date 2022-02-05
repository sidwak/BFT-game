using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RackBoxHolderScript : MonoBehaviour
{
    public int boxCountnum = 0;
    public int maxBoxCount;

    public bool isBuyerAiTarget = false;
    private bool isPlayerInArea = false;
    private bool isMoverInArea = false;

    public Vector2 offset;
    public Vector2 canvasScale;

    public GameObject CoinPrefab;

    public PlayerBoxScript playerBoxScript;
    public CanvasScript canvasScript;
    private List<GameObject> rackBoxesList = new List<GameObject>();
    private MoverBoxScript moverBoxScript;
    private BuyerAiScript buyerAiScript;

    public GameObject RackTextMainObject;
    public TextMeshProUGUI rackText;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            rackBoxesList.Add(child.gameObject);
        }
        for (int i = 0; i < rackBoxesList.Count; i++)
        {
            rackBoxesList[i].SetActive(false);
        }
        for (int i = 0; i < boxCountnum; i++)
        {
            rackBoxesList[i].SetActive(true);
        }
        RackTextMainObject.SetActive(true);
        canvasScale = new Vector2(1080f / Screen.width, 1920f / Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 numPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z));
        Vector2 Posnum;
        Posnum.x = numPos.x;
        Posnum.y = numPos.y;
        Posnum.x *= canvasScale.x;
        Posnum.y *= canvasScale.y;
        //Posnum += offset;
        RackTextMainObject.GetComponent<RectTransform>().anchoredPosition = Posnum;

        rackText.text = boxCountnum.ToString() + "/" + maxBoxCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BuyerAI")
        {
            if (isMoverInArea)
            {
                return;
            }
            moverBoxScript = other.GetComponentInChildren<MoverBoxScript>();
            buyerAiScript = other.GetComponent<BuyerAiScript>();
            isMoverInArea = true;
            StartCoroutine(MoverStay());
            return;
        }
        isPlayerInArea = true;
        StartCoroutine(PlayerStay());
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMoverInArea && other.gameObject.name == "BuyerAI")
        {
            isMoverInArea = false;
        }
        else
        {
            isPlayerInArea = false;
        }
    }

    IEnumerator PlayerStay()
    {
        yield return new WaitForSeconds(0.075f);
        if (playerBoxScript.breadCount == 0 || boxCountnum == maxBoxCount)
        {
            yield break;
        }
        int curBox = playerBoxScript.boxNumber - 1;
        canvasScript.totalBoxes--;
        playerBoxScript.PlayerBoxesList[curBox].SetActive(false);
        GameObject refBox = playerBoxScript.PlayerBoxesList[curBox];
        playerBoxScript.boxNumber--;
        playerBoxScript.breadCount--;
        GameObject numBox = Instantiate(playerBoxScript.PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
        numBox.SetActive(true);
        //GameObject numCoin = Instantiate(CoinPrefab, numBox.transform.position, CoinPrefab.transform.rotation);
        //numCoin.GetComponent<CoinAnimationScript>().endTarget = coinEndPostion;
        numBox.AddComponent<BoxScript>();
        numBox.GetComponent<BoxScript>().targetobject = rackBoxesList[boxCountnum];
        numBox.GetComponent<BoxScript>().isTargetset = true;
        boxCountnum++;
        if (isPlayerInArea)
        {
            StartCoroutine(PlayerStay());
        }
    }

    IEnumerator MoverStay()
    {
        yield return new WaitForSeconds(0.15f);
        if (buyerAiScript.isMoving)
        {
            StartCoroutine(MoverStay());
            yield break;
        }
        if (moverBoxScript.boxNumber == 5 || boxCountnum == 0)
        {
            yield break;
        }
        GameObject refBox = null;
        int pos = 0;
        for (int i = rackBoxesList.Count - 1; i >= 0; i--)
        {          
            if (rackBoxesList[i].activeInHierarchy)
            {
                refBox = rackBoxesList[i];
                pos = i;
                break;
            }
        }
        if (refBox == null)
        {
            StartCoroutine(MoverStay());
            yield break;
        }
        //GameObject num = Instantiate(activeBoxesList[0], activeBoxesList[activeBoxesList.Count-1].transform.position, activeBoxesList[0].transform.rotation);
        GameObject num = Instantiate(rackBoxesList[pos], refBox.transform.position, rackBoxesList[pos].transform.rotation);
        num.transform.localScale = Vector3.one;
        num.AddComponent<BoxScript>();
        int numbox = moverBoxScript.boxNumber;
        num.GetComponent<BoxScript>().targetobject = moverBoxScript.PlayerBoxesList[numbox];
        num.GetComponent<BoxScript>().isTargetset = true;
        num.GetComponent<BoxScript>().isCarbox = true;
        num.GetComponent<BoxScript>().CoinPrefab = CoinPrefab;
        moverBoxScript.boxNumber++;
        rackBoxesList[boxCountnum-1].SetActive(false);
        canvasScript.totalCoins++;
        if (boxCountnum > 0)
        {
            boxCountnum--;
        }       
        if (isMoverInArea)
        {
            StartCoroutine(MoverStay());
        }
    }
}
