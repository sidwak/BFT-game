using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarBoxesScript : MonoBehaviour
{

    public int boxCountnum = 25;
    public int maxBoxCount;
    public int coinTakenCount = 0;

    public float amountBox;

    public bool playerInArea = false;
    public bool isConvertedCar = false;
    public bool isMoverInArea = false;
    public bool isLeavedCalled = false;
    public bool isAiTarget = false;

    public Vector2 offset;
    public Vector2 canvasScale;

    public GameObject playerBoxHolder;
    public GameObject car;
    public GameObject character;
    public GameObject canvas;
    public GameObject CoinPrefab;
    public GameObject CoinPrefab2;
    public GameObject coinEndPostion;

    public PlayerBoxScript mainBoxScript;
    public PlayerBoxScript cartBoxScript;
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
            if (isConvertedCar)
            {
                return;
            }
            moverBoxScript = other.GetComponentInChildren<MoverBoxScript>();
            isMoverInArea = true;
            StartCoroutine(MoverStay());
            return;
        }
        playerInArea = true;
        if (!isConvertedCar)
        {
            StartCoroutine(PlayerStay());
        }
        else
        {
            StartCoroutine(CartPlayerStay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMoverInArea && other.gameObject.name == "MoverAI")
        {
            isMoverInArea = false;
        }
        else
        {
            playerInArea = false;
            StartCoroutine(PlayCoinAnimation());
        }
    }

    IEnumerator PlayerStay()
    {
        yield return new WaitForSeconds(0.075f);
        if (playerBoxHolder.GetComponent<PlayerBoxScript>().breadCount == 0 || boxCountnum == maxBoxCount)
        {
            yield break;
        }
        //int curBox = playerBoxHolder.GetComponent<PlayerBoxScript>().boxNumber - 1;
        int curBox = 0;
        for (int i = 0; i < mainBoxScript.PlayerBoxesList.Count; i++)
        {
            if (mainBoxScript.PlayerBoxesList[i].activeInHierarchy)
            {
                curBox = i;
            }
        }
        canvas.GetComponent<CanvasScript>().totalBoxes--;
        canvas.GetComponent<CanvasScript>().totalCoins += amountBox;
        mainBoxScript.PlayerBoxesList[curBox].SetActive(false);
        GameObject refBox = mainBoxScript.PlayerBoxesList[curBox];
        mainBoxScript.boxNumber--;
        mainBoxScript.breadCount--;
        GameObject numBox = Instantiate(mainBoxScript.PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
        numBox.SetActive(true);
        numBox.AddComponent<BoxScript>();
        numBox.GetComponent<BoxScript>().targetobject = carBoxesList[boxCountnum];
        numBox.GetComponent<BoxScript>().isTargetset = true;
        ////numBox.GetComponent<BoxScript>().isCarbox = true;
        ////numBox.GetComponent<BoxScript>().CoinPrefab = CoinPrefab;
        //Instantiate(CoinPrefab, carBoxesList[boxCountnum].transform.position, CoinPrefab.transform.rotation, canvas.transform);
        boxCountnum++;
        coinTakenCount++;
        if (coinTakenCount >= 10)
        {
            StartCoroutine(PlayCoinAnimation());
        }
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
        canvas.GetComponent<CanvasScript>().totalCoins += amountBox;
        moverBoxScript.PlayerBoxesList[curBox].SetActive(false);
        GameObject refBox = moverBoxScript.PlayerBoxesList[curBox];
        moverBoxScript.boxNumber--;
        GameObject numBox = Instantiate(moverBoxScript.PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
        numBox.SetActive(true);
        numBox.AddComponent<BoxScript>();
        numBox.GetComponent<BoxScript>().targetobject = carBoxesList[boxCountnum];
        numBox.GetComponent<BoxScript>().isTargetset = true;
        //numBox.GetComponent<BoxScript>().isCarbox = true;
        //numBox.GetComponent<BoxScript>().CoinPrefab = CoinPrefab;
        boxCountnum++;
        if (isMoverInArea)
        {
            StartCoroutine(MoverStay());
        }
    }

    IEnumerator CartPlayerStay()
    {
        yield return new WaitForSeconds(0.075f);
        if (cartBoxScript.boxNumber == 0 || boxCountnum == maxBoxCount)
        {
            yield break;
        }
        int curBox = cartBoxScript.boxNumber - 1;
        canvas.GetComponent<CanvasScript>().totalCoins += amountBox;
        cartBoxScript.PlayerBoxesList[curBox].SetActive(false);
        GameObject refBox = cartBoxScript.PlayerBoxesList[curBox];
        cartBoxScript.boxNumber--;
        GameObject numBox = Instantiate(cartBoxScript.PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
        numBox.transform.localScale = new Vector3(1050f, 1050f, 600f);
        numBox.SetActive(true);
        numBox.AddComponent<BoxScript>();
        numBox.GetComponent<BoxScript>().targetobject = carBoxesList[boxCountnum];
        numBox.GetComponent<BoxScript>().isTargetset = true;
        numBox.GetComponent<BoxScript>().isCarbox = true;
        numBox.GetComponent<BoxScript>().CoinPrefab = CoinPrefab;
        boxCountnum++;
        if (playerInArea)
        {
            StartCoroutine(CartPlayerStay());
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

    IEnumerator PlayCoinAnimation()
    {
        if (coinTakenCount == 0)
        {
            yield break;
        }
        CoinPrefab2.GetComponentInChildren<TextMeshPro>().text = "+" + coinTakenCount.ToString();
        Instantiate(CoinPrefab2, new Vector3(transform.position.x-1f, transform.position.y + 3f, transform.position.z), CoinPrefab2.transform.rotation);
        int numCoinTaken = coinTakenCount;
        coinTakenCount = 0;
        for (int i = 0; i < numCoinTaken; i++)
        {
            Instantiate(CoinPrefab, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), CoinPrefab.transform.rotation, canvas.transform);
            yield return new WaitForSeconds(0.055f);
        }
        
    }
}
