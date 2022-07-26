using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    public bool isTargetset = false;
    public bool isCarbox = false;
    public bool isCalled = false;

    //public Vector3 target;

    //public Quaternion rotationTarget;

    public GameObject targetobject;
    public GameObject CoinPrefab;
    public GameObject coinendposition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetset)
        {
            //transform.position = Vector3.Lerp(transform.position, targetobject.transform.position, 20f * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetobject.transform.rotation, 20f * Time.deltaTime);
            if (!isCalled)
            {
                StartCoroutine(LerpPosition(targetobject.transform.position, 0.25f));
                StartCoroutine(LerpRotation(targetobject.transform.rotation, 0.25f));
                isCalled = true;
            }
            if (targetobject == null)  /// STILL ERROR EXISTS
            {
                Destroy(gameObject);
            }
        }
        if (targetobject != null)
        {
            if (Vector3.Distance(transform.position, targetobject.transform.position) < 1f)
            {
                isTargetset = false;
                targetobject.SetActive(true);
                if (isCarbox)
                {
                    GameObject num = Instantiate(CoinPrefab, transform.position, CoinPrefab.transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            if (targetobject != null)
            {
                targetPosition = targetobject.transform.position;
            }
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        if (targetobject != null)
        {
            transform.position = targetobject.transform.position;
        }
        isFinished();
    }
    IEnumerator LerpRotation(Quaternion targetPosition, float duration)
    {
        float time = 0;
        Quaternion startPosition = transform.rotation;
        while (time < duration)
        {
            if (targetobject != null)
            {
                targetPosition = targetobject.transform.rotation;
            }
            transform.rotation = Quaternion.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        if (targetobject != null)
        {
            transform.rotation = targetobject.transform.rotation;
        }
    }

    public void isFinished()
    {
        if (targetobject != null)
        {
            targetobject.SetActive(true);
        }
        Destroy(gameObject);
    }
}
