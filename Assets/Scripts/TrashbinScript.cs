using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashbinScript : MonoBehaviour
{
    public bool playerInArea = false;

    public CharacterScript player;
    public CanvasScript canvas;
    public PlayerBoxScript mainBoxScript;
    public PlayerBoxScript ironBoxScript;
    public PlayerBoxScript cartBoxScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(0.075f);
        if (player.isMoving)
        {
            if (playerInArea)
            {
                StartCoroutine(PlayerStay());
            }
            yield break;
        }
        if (cartBoxScript.boxNumber > 0)
        {
            int cur3Box = cartBoxScript.boxNumber;
            for (int i = 0; i < cartBoxScript.PlayerBoxesList.Count; i++)
            {
                if (cartBoxScript.PlayerBoxesList[i].activeInHierarchy)
                {
                    cur3Box = i;
                    cartBoxScript.PlayerBoxesList[cur3Box].SetActive(false);
                    GameObject ref2Box = cartBoxScript.PlayerBoxesList[cur3Box];
                    GameObject num2Box = Instantiate(cartBoxScript.PlayerBoxesList[0], ref2Box.transform.position, ref2Box.transform.rotation);
                    num2Box.SetActive(true);
                    num2Box.AddComponent<BoxScript>();
                    num2Box.GetComponent<BoxScript>().targetobject = gameObject;
                    num2Box.GetComponent<BoxScript>().isTargetset = true;
                    cartBoxScript.boxNumber--;
                }
            }          
        }
        if (mainBoxScript.breadCount == 0 && ironBoxScript.ironCount == 0)
        {
            yield break;
        }
        if (mainBoxScript.breadCount > 0)
        {
            int curBox = 0;
            for (int i = 0; i < mainBoxScript.PlayerBoxesList.Count; i++)
            {
                if (mainBoxScript.PlayerBoxesList[i].activeInHierarchy)
                {
                    curBox = i;
                }
            }
            canvas.totalBoxes--;
            mainBoxScript.PlayerBoxesList[curBox].SetActive(false);
            GameObject refBox = mainBoxScript.PlayerBoxesList[curBox];
            GameObject numBox = Instantiate(mainBoxScript.PlayerBoxesList[0], refBox.transform.position, refBox.transform.rotation);
            numBox.SetActive(true);
            numBox.AddComponent<BoxScript>();
            numBox.GetComponent<BoxScript>().targetobject = gameObject;
            numBox.GetComponent<BoxScript>().isTargetset = true;
        }

        if (ironBoxScript.ironCount > 0)
        {
            int cur2Box = 0;
            for (int i = 0; i < ironBoxScript.PlayerBoxesList.Count; i++)
            {
                if (ironBoxScript.PlayerBoxesList[i].activeInHierarchy)
                {
                    cur2Box = i;
                }
            }
            canvas.totalIrons--;
            ironBoxScript.PlayerBoxesList[cur2Box].SetActive(false);
            GameObject ref2Box = ironBoxScript.PlayerBoxesList[cur2Box];
            GameObject num2Box = Instantiate(ironBoxScript.PlayerBoxesList[0], ref2Box.transform.position, ref2Box.transform.rotation);
            num2Box.SetActive(true);
            num2Box.AddComponent<BoxScript>();
            num2Box.GetComponent<BoxScript>().targetobject = gameObject;
            num2Box.GetComponent<BoxScript>().isTargetset = true;
        }
        
        if (mainBoxScript.breadCount > 0)
        {
            mainBoxScript.breadCount--;
            mainBoxScript.boxNumber--;
        }
        if (ironBoxScript.ironCount > 0)
        {
            ironBoxScript.ironCount--;
            mainBoxScript.boxNumber--;
        }
        if (playerInArea)
        {
            StartCoroutine(PlayerStay());
        }
    }
}
