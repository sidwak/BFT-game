using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 zone2;
    public Vector3 zone3;
    public Vector3 zone4;
    public Vector3 zone5;
    public Vector3 level2;
    public Vector3 startPos;

    public CharacterScript player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Zone1Lerp()
    {
        player.isLerping = true;
        StartCoroutine(LerpPosition(zone2, 2f));
    }

    public void Zone2Lerp()
    {
        player.isLerping = true;
        StartCoroutine(LerpPosition(zone3, 2f));
    }

    public void Zone3Lerp()
    {
        player.isLerping = true;
        StartCoroutine(LerpPosition(zone4, 2f));
    }

    public void Zone4Lerp()
    {
        player.isLerping = true;
        StartCoroutine(LerpPosition(zone5, 2f));
    }

    public void Zone5Lerp()
    {
        player.isLerping = true;
        StartCoroutine(LerpPosition(level2, 2f));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        startPos = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        StartCoroutine(WaitForTime());
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LerpBack(startPos, 2f));
    }

    IEnumerator LerpBack(Vector3 targetPosition, float duration)
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
        player.isLerping = false;
    }
}
