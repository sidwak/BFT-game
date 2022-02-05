using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VendingMachineScript : MonoBehaviour
{
    public int numCoins = 0;
    public int maxCoins = 50;
    public int coinTakenCount = 0;

    public bool playerInArea = false;

    public GameObject CoinPrefab;
    public GameObject CoinPrefab2;

    public TextMeshPro amountText;

    public CanvasScript canvas;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoinEvery());
    }

    // Update is called once per frame
    void Update()
    {
        amountText.text = numCoins.ToString() + "/" + maxCoins.ToString();
    }

    IEnumerator CoinEvery()
    {
        yield return new WaitForSeconds(1f);
        if (numCoins < maxCoins)
        {
            numCoins++;
        }
        StartCoroutine(CoinEvery());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        playerInArea = true;
        StartCoroutine(PlayerStay());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        playerInArea = false;
    }

    IEnumerator PlayerStay()
    {
        coinTakenCount = numCoins;
        canvas.totalCoins += numCoins;
        numCoins = 0;       
        StartCoroutine(PlayCoinAnimation()); 
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator PlayCoinAnimation()
    {
        if (coinTakenCount == 0)
        {
            yield break;
        }
        CoinPrefab2.GetComponentInChildren<TextMeshPro>().text = "+" + coinTakenCount.ToString();
        Instantiate(CoinPrefab2, new Vector3(transform.position.x - 1f, transform.position.y + 3f, transform.position.z), CoinPrefab2.transform.rotation);
        for (int i = 0; i < coinTakenCount; i++)
        {
            Instantiate(CoinPrefab, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), CoinPrefab.transform.rotation, canvas.transform);
            yield return new WaitForSeconds(0.055f);
        }
        coinTakenCount = 0;
    }
}
