using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarBoxesScript : MonoBehaviour
{

    public int boxCountnum = 25;
    public int maxBoxCount;

    public float amountBox;

    public bool playerInArea = false;
    private bool isMoverInArea = false;
    public bool isLeavedCalled = false;
    public bool isAiTarget = false;

    public Vector2 offset;
    public Vector2 canvasScale;

    public GameObject playerBoxHolder;
    public GameObject car;
    public GameObject character;
    public GameObject canvas;
    public GameObject CoinPrefab;
    public GameObject coinEndPostion;

    private MoverBoxScript moverBoxScript;

    public GameObject CarTextObject;

    public TextMeshProUGUI BoxCountText;

    private List<GameObject> carBoxesList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            carBoxesList.Add(child.gameObject);
        }
        for (int i = 0; i < carBoxesList.Count; i++)
        {
            carBoxesList[i].SetActive(false);
        }
        for (int i = 0; i < boxCountnum; i++)
        {
            carBoxesList[i].SetActive(true);
        }

        CarTextObject.SetActive(true);
        /*Vector3 numCarPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 numOffset;
        numOffset.x = numCarPos.x;
        numOffset.y = numCarPos.y;
        offset = CarTextObject.GetComponent<RectTransform>().anchoredPosition - numOffset;*/
        //Debug.Log(offset);
        //offset = new Vector2(13.6f, 177.8f);

        canvasScale = new Vector2(1080f / Screen.width, 1920f / Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCountnum == maxBoxCount && !isLeavedCalled)
        {
            car.GetComponent<CarScript>().StartLeave();
            isLeavedCalled = true;
            CarTextObject.SetActive(false);
        }

        Vector3 numPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z));
        Vector2 Posnum;
        Posnum.x = numPos.x;
        Posnum.y = numPos.y;
        Posnum.x *= canvasScale.x;
        Posnum.y *= canvasScale.y;
        //Posnum += offset;
        CarTextObject.GetComponent<RectTransform>().anchoredPosition = Posnum;

        BoxCountText.text = boxCountnum.ToString() + "/" + maxBoxCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MoverAI")
        {
            moverBoxScript = other.GetComponentInChildren<MoverBoxScript>();
            isMoverInArea = true;
            StartCoroutine(MoverStay());
            return;
        }
        playerInArea = true;
        StartCoroutine(PlayerStay());
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMoverInArea)
        {
            isMoverInArea = false;
        }
        playerInArea = false;
    }

    IEnumerator PlayerStay()
    {
        yield return new WaitForSeconds(0.075f);
        if (playerBoxHolder.GetComponent<PlayerBoxScript>().boxNumber == 0 || boxCountnum == maxBoxCount)
        {
            yield break;
        }
        int curBox = playerBoxHolder.GetComponent<PlayerBoxScript>().boxNumber - 1;
        canvas.GetComponent<CanvasScript>().totalBoxes--;
        canvas.GetComponent<CanvasScript>().totalCoins += amountBox;
        playerBoxHolder.GetComponent<PlayerBoxScript>().PlayerBoxesList[curBox].SetActive(false);
        GameObject refBox = playerBoxHolder.GetComponent<PlayerBoxScript>().PlayerBoxesList[curBox];
        playerBoxHolder.GetComponent<PlayerBoxScript>().boxNumber--;
        GameObject numBox = Instantiate(playerBoxHolder.GetComponent<PlayerBoxScript>().PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
        numBox.SetActive(true);
        //GameObject numCoin = Instantiate(CoinPrefab, numBox.transform.position, CoinPrefab.transform.rotation);
        //numCoin.GetComponent<CoinAnimationScript>().endTarget = coinEndPostion;
        numBox.AddComponent<BoxScript>();
        numBox.GetComponent<BoxScript>().targetobject = carBoxesList[boxCountnum];
        numBox.GetComponent<BoxScript>().isTargetset = true;
        numBox.GetComponent<BoxScript>().isCarbox = true;
        numBox.GetComponent<BoxScript>().CoinPrefab = CoinPrefab;
        boxCountnum++;
        if (playerInArea)
        {
            StartCoroutine(PlayerStay());
        }
    }

    IEnumerator MoverStay()
    {
        yield return new WaitForSeconds(0.075f);
        if (moverBoxScript.boxNumber == 0 || boxCountnum == maxBoxCount)
        {
            yield break;
        }
        int curBox = moverBoxScript.boxNumber - 1;
        canvas.GetComponent<CanvasScript>().totalBoxes--;
        canvas.GetComponent<CanvasScript>().totalCoins += amountBox;
        moverBoxScript.PlayerBoxesList[curBox].SetActive(false);
        GameObject refBox = moverBoxScript.PlayerBoxesList[curBox];
        moverBoxScript.boxNumber--;
        GameObject numBox = Instantiate(moverBoxScript.PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
        numBox.SetActive(true);
        //GameObject numCoin = Instantiate(CoinPrefab, numBox.transform.position, CoinPrefab.transform.rotation);
        //numCoin.GetComponent<CoinAnimationScript>().endTarget = coinEndPostion;
        numBox.AddComponent<BoxScript>();
        numBox.GetComponent<BoxScript>().targetobject = carBoxesList[boxCountnum];
        numBox.GetComponent<BoxScript>().isTargetset = true;
        numBox.GetComponent<BoxScript>().isCarbox = true;
        numBox.GetComponent<BoxScript>().CoinPrefab = CoinPrefab;
        boxCountnum++;
        if (isMoverInArea)
        {
            StartCoroutine(MoverStay());
        }
    }

    public void ResetBoxes()
    {
        for (int i = 0; i < carBoxesList.Count; i++)
        {
            carBoxesList[i].SetActive(false);
        }
        boxCountnum = 0;
        CarTextObject.SetActive(true);
    }
}
