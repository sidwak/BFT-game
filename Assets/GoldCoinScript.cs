using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinScript : MonoBehaviour
{
    public Vector3 targetCoinPos;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(LerpPosition(targetCoinPos, 0.4f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = rectTransform.anchoredPosition3D;
        while (time < duration)
        {
            rectTransform.anchoredPosition3D = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition3D = targetCoinPos;
        Destroy(gameObject);
    }
}
