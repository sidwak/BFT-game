using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float carLeaveSpeed = 2f;
    public float carEndSpeed = 2f;
    public float carWaitTime = 5f;

    public bool readyLeave = false;
    public bool endReady = false;
    private bool isEndCalled = false;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public GameObject carBoxHolder;
    public GameObject confetti;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyLeave)
        {
            //transform.position = Vector3.Lerp(transform.position, endPosition, 0.25f * Time.deltaTime);
            StartCoroutine(LerpPosition(endPosition, 2.5f));
            readyLeave = false;
        }
        //if (Vector3.Distance(transform.position, endPosition) < 0.1f)
        if (transform.position == endPosition && !isEndCalled)
        {
            readyLeave = false;
            carBoxHolder.GetComponent<CarBoxesScript>().ResetBoxes();
            endReady = true;
        }
        if (endReady)
        {
            //transform.position = Vector3.Lerp(transform.position, startPosition, 0.25f * Time.deltaTime);
            StartCoroutine(DelayEndLeave());
            endReady = false;
            isEndCalled = true;
        }
        if (Vector3.Distance(transform.position, startPosition) < 0.05f)
        {
            endReady = false;
            isEndCalled = false;
            transform.position = startPosition;
            carBoxHolder.GetComponent<CarBoxesScript>().isLeavedCalled = false;
        }
    }

    public void StartLeave()
    {
        confetti.SetActive(true);
        StartCoroutine(DelayStartLeave());
    }

    IEnumerator DelayStartLeave()
    {
        yield return new WaitForSeconds(0.5f);
        
        readyLeave = true;
    }

    IEnumerator DelayEndLeave()
    {
        yield return new WaitForSeconds(carWaitTime);
        confetti.SetActive(false);
        StartCoroutine(LerpPosition(startPosition, 2.5f));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

}
