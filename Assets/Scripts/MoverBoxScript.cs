using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBoxScript : MonoBehaviour
{
    public int boxNumber = 0;

    public List<GameObject> PlayerBoxesList = new List<GameObject>();

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

    }
}
