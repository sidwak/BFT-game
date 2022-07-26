using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockZoneScript : MonoBehaviour
{
    public GameObject UnlockZone_UI;

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
        UnlockZone_UI.SetActive(true);
        StartCoroutine(LerpPosition2(Vector3.one, 0.25f));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "MainPlayer")
        {
            return;
        }
        UnlockZone_UI.SetActive(false);
    }

    IEnumerator LerpPosition2(Vector3 targetPosition, float duration)
    {
        float time = 0;
        UnlockZone_UI.transform.localScale = Vector3.zero;
        Vector3 startPosition = Vector3.zero;
        while (time < duration)
        {
            UnlockZone_UI.transform.localScale = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        UnlockZone_UI.transform.localScale = targetPosition;
    }
}
